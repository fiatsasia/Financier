using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financier.Trading
{
    public abstract class MarketBase
    {
        /*

        event EventHandler<IPositionEventArgs> PositionChanged;

        void Open();
        IObservable<ITicker> GetTickerSource();

        IOrderTransaction PlaceOrder(OrderRequest request);
        */
        public event EventHandler<OrderTransactionEventArgs> OrderTransactionChanged;
        public event EventHandler<OrderPositionEventArgs> PositionChanged;

        public virtual void InvokeOrderTransactionChanged(object sender, OrderTransactionEventArgs ev) => OrderTransactionChanged?.Invoke(sender, ev);
        public virtual void InvokePositionChanged(object sender, OrderPositionEventArgs ev) => PositionChanged?.Invoke(sender, ev);

        public abstract string ExchangeCode { get; }
        public abstract string MarketCode { get; }
        public abstract decimal MinimumOrderSize { get; }
        public abstract decimal BestAskPrice { get; }
        public abstract decimal BestBidPrice { get; }
        public abstract decimal MidPrice { get; }
        public abstract decimal LastTradedPrice { get; }
        public abstract bool HasActiveOrder { get; }
        public abstract OrderPositions Positions { get; }
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

        public OrderRequest ConvertPrimitive(OrderRequest req)
        {
            var result = default(OrderRequest);
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
                        result = new OrderRequest((req.OrderSize > 0m) ? OrderType.TriggerPriceAbove : OrderType.TriggerPriceBelow, new OrderRequest[] { OrderFactory.Market(req.OrderSize.Value) })
                        {
                            TriggerPrice = req.TriggerPrice,
                            TriggerPriceType = req.TriggerPriceType,
                            TriggerPriceOffset = req.TriggerPriceOffset
                        };
                    }
                    break;

                case OrderType.StopLimit:
                    {
                        var child = new OrderRequest(OrderType.Limit)
                        {
                            OrderSize = req.OrderSize,
                            OrderPrice = req.OrderPrice,
                            OrderPriceType = req.OrderPriceType,
                            OrderPriceOffset = req.OrderPriceOffset,
                        };
                        result = new OrderRequest((req.OrderSize > 0m) ? OrderType.TriggerPriceAbove : OrderType.TriggerPriceBelow, new OrderRequest[] { child })
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
                    result = new OrderRequest(OrderType.TriggerTrailingOffset, OrderFactory.Market(req.OrderSize.Value))
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
                        var child = new OrderRequest(OrderType.Limit)
                        {
                            OrderSize = req.OrderSize,
                            OrderPrice = req.OrderPrice,
                            OrderPriceType = req.OrderPriceType,
                            OrderPriceOffset = req.OrderPriceOffset,
                        };
                        result = new OrderRequest(OrderType.TriggerTrailingOffset, child)
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

                    result = new OrderRequest(req.OrderSize > 0m ? OrderType.TriggerPriceBelow : OrderType.TriggerPriceAbove, OrderFactory.Market(req.OrderSize.Value))
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

        protected virtual void ApplyPrices(OrderRequest order)
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

        public void ApplyExecutedPrice(OrderRequest order, decimal? executedPrice)
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

        public abstract OrderTransactionBase CreateTransaction(MarketBase market, OrderRequest order, OrderTransactionBase parent);

        public virtual OrderTransactionBase PlaceOrder(OrderRequest order, OrderTransactionBase parent)
        {
            var tx = CreateTransaction(this, order, parent);
            tx.ChangeOrderState(OrderState.Outstanding);
            switch (order.OrderType)
            {
                case OrderType.NullOrder:
                    tx.ChangeOrderState(OrderState.Ordered);
                    tx.ProcessEvent(OrderTransactionEventType.Ordered);
                    Task.Run(() =>
                    {
                        tx.ChangeOrderState(OrderState.Completed);
                        tx.ProcessEvent(OrderTransactionEventType.Completed);
                    });
                    break;

                case OrderType.IFD:
                    tx.DispatchEvent(OrderTransactionEventType.StartOrdering);
                    PlaceOrder(order.Children[0], tx);
                    break;

                case OrderType.TriggerPriceAbove:
                case OrderType.TriggerPriceBelow:
                case OrderType.TriggerTrailingOffset:
                    tx.ChangeOrderState(OrderState.Ordered);
                    tx.ProcessEvent(OrderTransactionEventType.Ordered);
                    break;

                case OrderType.OCO:
                    tx.DispatchEvent(OrderTransactionEventType.StartOrdering);
                    Parallel.ForEach(order.Children, childOrderRequest => PlaceOrder(childOrderRequest, tx));
                    break;

                default:
                    throw new NotSupportedException();
            }
            //Log.Trace($"FxMarket order placed type:{order.OrderType}");

            return tx;
        }

        public virtual OrderTransactionBase PlaceOrder(OrderRequest order) => PlaceOrder(order, default);
    }
}
