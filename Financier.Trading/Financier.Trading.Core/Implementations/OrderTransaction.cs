//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace Financier.Trading
{
    public class OrderTransaction : OrderTransactionBase
    {
        CompositeDisposable _disposables = new CompositeDisposable();
        decimal _referencePriceMax = decimal.MinValue;
        decimal _referencePriceMin = decimal.MaxValue;

        object _ocoLock = new object();
        int _ocoOrdered = 0;
        int _ocoCompleted = 0;
        Timer _expirationTimer;

        public OrderTransaction(MarketBase market, OrderRequest order, OrderTransactionBase parent)
            : base(market, order, parent)
        {
            if (order.OrderType.IsTriggerPrice())
            {
                market.GetTickerSource().Subscribe(OnTickerChanged).AddTo(_disposables);
            }

            if (Order.TimeToExpire.HasValue)
            {
                _expirationTimer = new Timer(_ =>
                {
                    if (IsCancelable)
                    {
                        _disposables.Dispose();
                        ChangeState(OrderTransactionState.Idle);
                        ChangeOrderState(OrderState.Expired);
                        ProcessEvent(OrderTransactionEventType.Expired);
                    }
                }, null, Order.TimeToExpire.Value, TimeSpan.Zero).AddTo(_disposables);
            }
        }

        void OnTickerChanged(ITicker ticker)
        {
            if (OrderState != OrderState.Ordered)
            {
                return;
            }

            decimal referencePrice = Order.TriggerPriceType switch
            {
                PriceType.LastTraded => ticker.LastTradedPrice,
                PriceType.BestAsk => ticker.BestAskPrice,
                PriceType.BestBid => ticker.BestBidPrice,
                PriceType.Mid => (ticker.BestAskPrice + ticker.BestBidPrice) / 2.0m,
                _ => throw new NotSupportedException()
            };
            _referencePriceMax = Math.Max(referencePrice, _referencePriceMax);
            _referencePriceMin = Math.Min(referencePrice, _referencePriceMin);

            if (Order.OrderType == OrderType.TriggerPriceAbove && referencePrice >= Order.TriggerPrice) { }
            else if (Order.OrderType == OrderType.TriggerPriceBelow && referencePrice <= Order.TriggerPrice) { }
            else if (Order.OrderType == OrderType.TriggerTrailingOffset)
            {
                if (Order.TrailingOffset > 0m) // Trail buy
                {
                    if (_referencePriceMin + Order.TrailingOffset <= referencePrice) { }
                    else return;
                }
                else // Trail sell
                {
                    if (_referencePriceMax + Order.TrailingOffset >= referencePrice) { }
                    else return;
                }
            }
            else
            {
                return;
            }

            _disposables.Dispose();
            ChangeOrderState(OrderState.Triggered);
            ProcessEvent(OrderTransactionEventType.Triggered);
            Task.Run(() => Market.PlaceOrder(Order.Children[0], this));
        }

        public override void Cancel()
        {
            Task.Run(() =>
            {
                if (Children.Count > 0)
                {
                    Children.ForEach(tx => tx.Cancel());
                    return;
                }

                _disposables.Dispose();
                ChangeState(OrderTransactionState.Idle);
                ChangeOrderState(OrderState.Canceled);
                ProcessEvent(OrderTransactionEventType.Canceled);
            });
        }

        public override void EscalteEvent(OrderTransactionBase txChild, OrderTransactionEventArgs ev)
        {
            var eventType = OrderTransactionEventType.Unknown;
            var childIndex = Children.ToList().FindIndex(e => e.Id == txChild.Id);
            switch (Order.OrderType)
            {
                case OrderType.IFD:
                    switch (childIndex)
                    {
                        case 0:
                            switch (ev.EventType)
                            {
                                case OrderTransactionEventType.Ordered:
                                    ChangeOrderState(OrderState.Ordered);
                                    eventType = OrderTransactionEventType.Ordered;
                                    break;

                                case OrderTransactionEventType.Executed:
                                    Market.ApplyExecutedPrice(Order.Children[1], Children[0].ExecutedPrice);
                                    ChangeOrderState(OrderState.Triggered);
                                    eventType = OrderTransactionEventType.Triggered;
                                    Task.Run(() => Market.PlaceOrder(Order.Children[1], this));
                                    break;

                                case OrderTransactionEventType.Canceled:
                                    ChangeOrderState(OrderState.Canceled);
                                    eventType = OrderTransactionEventType.Canceled;
                                    break;

                                case OrderTransactionEventType.Expired: // Expired if order
                                    ChangeOrderState(OrderState.Expired);
                                    eventType = OrderTransactionEventType.Expired;
                                    break;
                            }
                            break;

                        case 1:
                            switch (ev.EventType)
                            {
                                case OrderTransactionEventType.Triggered:
                                    // Nothing to do
                                    break;

                                case OrderTransactionEventType.Executed:
                                case OrderTransactionEventType.Completed:
                                    ChangeOrderState(OrderState.Completed);
                                    ChangeState(OrderTransactionState.Closed);
                                    eventType = OrderTransactionEventType.Completed;
                                    break;

                                case OrderTransactionEventType.Canceled:
                                    ChangeOrderState(OrderState.Canceled);
                                    eventType = OrderTransactionEventType.Canceled;
                                    break;
                            }
                            break;

                        default:
                            throw new InvalidOperationException();
                    }
                    break;

                case OrderType.TriggerPriceAbove:
                case OrderType.TriggerPriceBelow:
                case OrderType.TriggerTrailingOffset:
                    switch (ev.EventType)
                    {
                        case OrderTransactionEventType.Triggered:
                            // Nothing to do
                            break;

                        case OrderTransactionEventType.Executed:
                        case OrderTransactionEventType.Completed:
                            ChangeOrderState(OrderState.Completed);
                            ChangeState(OrderTransactionState.Closed);
                            eventType = OrderTransactionEventType.Completed;
                            break;

                        case OrderTransactionEventType.Canceled:
                            ChangeOrderState(OrderState.Canceled);
                            eventType = OrderTransactionEventType.Canceled;
                            break;
                    }
                    break;

                case OrderType.OCO:
                    lock (_ocoLock)
                    {
                        var triggered = false;
                        switch (ev.EventType)
                        {
                            case OrderTransactionEventType.Ordered:
                                if (++_ocoOrdered == Order.Children.Count)
                                {
                                    ChangeOrderState(OrderState.Ordered);
                                    eventType = OrderTransactionEventType.Ordered;
                                }
                                break;

                            case OrderTransactionEventType.Triggered:
                                if (_ocoCompleted == 0 && OrderState != OrderState.Triggered)
                                {
                                    triggered = true;
                                }
                                break;

                            case OrderTransactionEventType.Executed:
                            case OrderTransactionEventType.Completed:
                            case OrderTransactionEventType.Canceled:
                                _ocoCompleted++;
                                if (_ocoCompleted == 1 && ev.EventType != OrderTransactionEventType.Canceled)
                                {
                                    triggered = true;
                                }
                                else if (_ocoCompleted == Order.Children.Count)
                                {
                                    if (Children.All(e => e.OrderState == OrderState.Canceled))
                                    {
                                        ChangeOrderState(OrderState.Canceled);
                                        eventType = OrderTransactionEventType.Canceled;
                                    }
                                    else
                                    {
                                        ChangeOrderState(OrderState.Completed);
                                        eventType = OrderTransactionEventType.Completed;
                                    }
                                }
                                break;

                            default:
                                break;
                        }

                        if (triggered)
                        {
                            ChangeOrderState(OrderState.Triggered);
                            eventType = OrderTransactionEventType.Triggered;
                            Parallel.For(0, Children.Count, i =>
                            {
                                if (i != childIndex)
                                {
                                    Children[i].Cancel();
                                }
                            });
                        }
                    }
                    break;
            }

            if (eventType != OrderTransactionEventType.Unknown)
            {
                DispatchEvent(eventType);
                if (Parent != null)
                {
                    Parent.EscalteEvent(this, new OrderTransactionEventArgs(DateTime.UtcNow, eventType, this));
                }
            }
        }
    }
}
