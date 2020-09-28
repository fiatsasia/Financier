//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Financier.Trading
{
    public class Order : IOrder
    {
        public virtual DateTime? OpenTime { get; set; }
        public virtual DateTime? CloseTime { get; set; }

        public virtual OrderType OrderType { get; set; }
        public virtual decimal? OrderSize { get; set; }
        public virtual decimal? OrderPrice { get; set; }

        public virtual decimal? TriggerPrice { get; set; }
        public virtual decimal? TrailingOffset { get; set; }

        public virtual IEnumerable<IExecution> Executions { get; } = new IExecution[0];
        public virtual decimal? ExecutedPrice { get; }
        public virtual decimal? ExecutedSize { get; }

        public virtual IReadOnlyList<IOrder> Children => _children;

        public virtual bool IsClosed => State == OrderState.Completed;

        public OrderPriceType OrderPriceType { get; set; }
        public decimal OrderPriceOffset { get; set; }

        public OrderPriceType TriggerPriceType { get; set; }
        public decimal TriggerPriceOffset { get; set; }

        public OrderPriceType ReferencePriceType { get; set; }
        public OrderTransactionEventType TriggerEventType { get; set; }

        OrderState _state;
        List<IOrder> _children;

        #region Constructors
        public Order()
        {
            _children = new List<IOrder>();
        }

        public Order(IOrder child)
        {
            _children = new List<IOrder> { child };
        }

        public Order(IEnumerable<IOrder> children)
        {
            _children = new List<IOrder>(children);
        }
        #endregion Constructors

        public virtual OrderState State
        {
            get
            {
                if (_children.Count == 0)
                {
                    return _state;
                }

                switch (OrderType)
                {
                    case OrderType.OCO:
                        if (Children.Any(e => e.State == OrderState.OrderFailed))
                        {
                            _state = OrderState.OrderFailed;
                        }
                        else if (Children.Any(e => e.State == OrderState.Ordering))
                        {
                            _state = OrderState.Ordering;
                        }
                        else if (Children.All(e => e.State == OrderState.Ordered))
                        {
                            _state = OrderState.Ordered;
                        }
                        else if (Children.All(e => e.State == OrderState.Canceled))
                        {
                            _state = OrderState.Canceled;
                        }
                        else if (Children.All(e => e.State == OrderState.Canceling || e.State == OrderState.Canceled))
                        {
                            _state = OrderState.Canceling;
                        }
                        else if (Children.All(e =>
                            e.State == OrderState.Canceled ||
                            e.State == OrderState.Executed ||
                            e.State == OrderState.Executing ||
                            e.State == OrderState.Completed ||
                            e.State == OrderState.Triggered
                        ))
                        {
                            _state = OrderState.Completed;
                        }
                        break;
                }

                return _state;
            }
            internal set
            {
                _state = value;
            }
        }

        public void ReplaceChildOrder(int childOrderIndex, IOrder order)
        {
            _children[childOrderIndex] = order;
        }

        public void ChangeState(OrderState newState)
        {
            if (State == newState)
            {
                return;
            }

            switch (newState)
            {
                case OrderState.Outstanding:
                    OpenTime = DateTime.UtcNow;
                    break;

                default:
                    if (newState.IsClosed())
                    {
                        CloseTime = DateTime.UtcNow;
                    }
                    break;
            }

            Debug.WriteLine($"Financier order status changed: {OrderType} {State} -> {newState}");
            State = newState;
        }
    }
}
