//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Financier.Trading
{
    public abstract class OrderTransactionBase
    {
        public Ulid Id => Order.Id;

        public OrderRequest Order { get; }

        public OrderTransactionState State { get; private set; }
        public OrderState OrderState => _orderStatus.OrderState;
        public virtual bool IsCancelable => State switch
        {
            OrderTransactionState.Idle => _orderStatus.IsCancelable,
            OrderTransactionState.Ordering => true,
            OrderTransactionState.Canceling => true,
            OrderTransactionState.Closed => false,
            _ => false
        };

        public OrderTransactionBase Parent { get; private set; }
        List<OrderTransactionBase> _children = new();
        public ReadOnlyCollection<OrderTransactionBase> Children { get; }
        public virtual DateTime OpenTime => _orderStatus.OpenTime;
        public virtual DateTime CloseTime => _orderStatus.CloseTime;
        public virtual DateTime Expiration => _orderStatus.Expiration;
        public decimal? ExecutedPrice { get; private set; }
        public decimal? ExecutedSize => Order.OrderSize.HasValue ? _execs.Sum(e => e.Price) : null;
        public string Metadata { get; }

        protected MarketBase Market { get; }
        OrderStatus _orderStatus;
        List<OrderExecution> _execs = new();

        protected OrderTransactionBase(MarketBase market, OrderRequest order, OrderTransactionBase parent)
        {
            Market = market;
            Order = order;
            Children = new(_children);
            Parent = parent;
            if (Parent != null)
            {
                Parent._children.Add(this);
            }

            _orderStatus = new OrderStatus();
        }

        public void ChangeOrderState(OrderState state)
        {
            _orderStatus.ChangeState(state);
        }

        protected void ChangeState(OrderTransactionState newState)
        {
            //Log.Trace($"Transaction status changed: {Order.OrderType} {State} -> {newState}");
            State = newState;
        }

        public virtual void OnOrderExecuted(DateTime time, decimal price, decimal size)
        {
            _execs.Add(new(time, price, size));
            if (ExecutedSize == Order.OrderSize.Value)
            {
                ChangeState(OrderTransactionState.Closed);
            }
        }

        public virtual void Cancel()
        {
            throw new NotImplementedException();
        }

        public virtual void EscalteEvent(OrderTransactionBase tx, OrderTransactionEventArgs e)
        {
        }

        public virtual void ProcessEvent(OrderTransactionEventType eventType)
        {
            DispatchEvent(eventType);
            if (Parent == null)
            {
                return;
            }
            Parent.EscalteEvent(this, new OrderTransactionEventArgs(DateTime.UtcNow, eventType, this));
        }

        public virtual void DispatchEvent(OrderTransactionEventType eventType)
        {
            throw new NotImplementedException();
            //_market.InvokeOrderTransactionChanged(this, new OrderTransactionEventArgs(DateTime.UtcNow, eventType, this));
        }
    }
}
