using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financier.Trading
{
    public abstract class MarketBase : IDisposable
    {
        public virtual decimal CurrentPrice { get; }

        public virtual Task<MarketDataSourcesBase> GetDataSourcesAsync() => throw new NotSupportedException();

        public abstract Task PlaceOrderAsync(Order order);
        public virtual Task CloseAllPositionsAsync() => throw new NotSupportedException();
        public virtual Task CancelAllTransactionAsync() => throw new NotSupportedException();
        public virtual OrderStatus GetOrderStatus(Ulid orderId) => throw new NotSupportedException();

        /*

        event EventHandler<IPositionEventArgs> PositionChanged;

        void Open();
        IObservable<ITicker> GetTickerSource();

        IOrderTransaction PlaceOrder(OrderRequest request);
        */
        public event EventHandler<OrderEventArgs>? OrderEvent;
        public event EventHandler<OrderPositionEventArgs> PositionChanged;

        protected virtual void RaiseOrderEvent(OrderEventArgs ev) => OrderEvent?.Invoke(this, ev);
        protected virtual void InvokePositionChanged(object sender, OrderPositionEventArgs ev) => PositionChanged?.Invoke(sender, ev);

        public abstract decimal MinimumOrderSize { get; }
        public abstract decimal BestAskPrice { get; }
        public abstract decimal BestBidPrice { get; }
        public abstract decimal MidPrice { get; }
        public abstract decimal LastTradedPrice { get; }
        public abstract int PriceDecimals { get; }

        public abstract void Open();
        public abstract IObservable<ITicker> GetTickerSource();

        decimal DecidePrice(PriceType? priceType, decimal orderSize) => priceType switch
        {
            PriceType.BestAsk => BestAskPrice,
            PriceType.BestBid => BestBidPrice,
            PriceType.Best => orderSize > 0m ? BestBidPrice : BestAskPrice,
            PriceType.LastTraded => LastTradedPrice,
            PriceType.Mid => MidPrice,
            _ => throw new NotSupportedException()
        };

        public Order ConvertPrimitive(Order req)
        {
            var result = default(Order);
            switch (req.OrderType)
            {
                case OrderType.IFD: // Executed trigger + order
                    result = OrderFactory.IFD(req.Children[0], req.Children[1]);
                    break;

                case OrderType.IFDOCO: // IFD + OCO
                    result = OrderFactory.IFD(req.Children[0], OrderFactory.OCO(req.Children[1], req.Children[2]));
                    break;

                case OrderType.Stop:
                    {
                        if (!req.OrderSize.HasValue)
                        {
                            throw new ArgumentException();
                        }
                        result = new Order((req.OrderSize > 0m) ? OrderType.TriggerPriceAbove : OrderType.TriggerPriceBelow, new Order[] { OrderFactory.Market(req.OrderSize.Value) })
                        {
                            TriggerPrice = req.TriggerPrice,
                            TriggerPriceType = req.TriggerPriceType,
                            TriggerPriceOffset = req.TriggerPriceOffset
                        };
                    }
                    break;

                case OrderType.StopLimit:
                    {
                        var child = new Order(OrderType.Limit)
                        {
                            OrderSize = req.OrderSize,
                            OrderPrice = req.OrderPrice,
                            OrderPriceType = req.OrderPriceType,
                            OrderPriceOffset = req.OrderPriceOffset,
                        };
                        result = new Order((req.OrderSize > 0m) ? OrderType.TriggerPriceAbove : OrderType.TriggerPriceBelow, new Order[] { child })
                        {
                            TriggerPrice = req.TriggerPrice,
                            TriggerPriceType = req.TriggerPriceType,
                            TriggerPriceOffset = req.TriggerPriceOffset
                        };
                    }
                    break;

                case OrderType.TrailingStop:
                    if (!req.OrderSize.HasValue || !req.TrailingOffset.HasValue)
                    {
                        throw new ArgumentException();
                    }
                    result = new Order(OrderType.TriggerTrailingOffset, OrderFactory.Market(req.OrderSize.Value))
                    {
                        TrailingOffset = req.OrderSize.Value > 0m ? req.TrailingOffset.Value : -req.TrailingOffset.Value
                    };
                    break;

                case OrderType.TrailingStopLimit:
                    {
                        if (!req.OrderSize.HasValue || !req.TrailingOffset.HasValue)
                        {
                            throw new ArgumentException();
                        }
                        var child = new Order(OrderType.Limit)
                        {
                            OrderSize = req.OrderSize,
                            OrderPrice = req.OrderPrice,
                            OrderPriceType = req.OrderPriceType,
                            OrderPriceOffset = req.OrderPriceOffset,
                        };
                        result = new Order(OrderType.TriggerTrailingOffset, child)
                        {
                            TrailingOffset = req.OrderSize.Value > 0m ? req.TrailingOffset.Value : -req.TrailingOffset.Value
                        };
                    }
                    break;

                case OrderType.TakeProfit:
                    if (!req.OrderSize.HasValue || !req.ProfitPrice.HasValue)
                    {
                        throw new ArgumentException();
                    }

                    result = new Order(req.OrderSize > 0m ? OrderType.TriggerPriceBelow : OrderType.TriggerPriceAbove, OrderFactory.Market(req.OrderSize.Value))
                    {
                        ProfitPrice = req.OrderSize > 0m ? -req.ProfitPrice.Value : req.ProfitPrice.Value
                    };
                    break;

                default:
                    result = req;
                    break;
            }

            return result;
        }

        protected virtual void ApplyPrices(Order order)
        {
            if (order.Children.Count > 0)
            {
                order.Children.ForEach(e => ApplyPrices(e));
            }

            if (order.OrderSize.HasValue && !order.OrderPrice.HasValue && order.OrderPriceType.HasValue)
            {
                order.OrderPrice = DecidePrice(order.OrderPriceType, order.OrderSize.Value);
                order.OrderPrice += order.OrderPriceOffset ?? 0m;
            }

            if (order.OrderSize.HasValue && !order.TriggerPrice.HasValue && order.TriggerPriceType.HasValue)
            {
                order.TriggerPrice = DecidePrice(order.TriggerPriceType, order.OrderSize.Value);
                order.TriggerPrice += order.TriggerPriceOffset ?? 0m;
            }
        }

        public void ApplyExecutedPrice(Order order, decimal? executedPrice)
        {
            if (order.Children.Count > 0)
            {
                order.Children.ForEach(e => ApplyExecutedPrice(e, executedPrice));
            }

            if (order.OrderPriceType == PriceType.LastTraded)
            {
                order.OrderPrice = executedPrice.HasValue ? executedPrice.Value : LastTradedPrice;
                order.OrderPrice += order.OrderPriceOffset;
            }

            if (order.TriggerPriceType == PriceType.LastTraded)
            {
                order.TriggerPrice = executedPrice.HasValue ? executedPrice.Value : LastTradedPrice;
                order.TriggerPrice += order.TriggerPriceOffset;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
