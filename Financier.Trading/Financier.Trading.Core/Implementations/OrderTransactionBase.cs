//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading;

public abstract class OrderTransactionBase : OrderStatus
{
    public Ulid Id => Order.Id;

    public OrderTransactionState State { get; private set; }

    public OrderTransactionBase Parent { get; private set; }
    List<OrderTransactionBase> _children = new();
    public ReadOnlyCollection<OrderTransactionBase> Children { get; }
    public string Metadata { get; }

    protected MarketBase Market { get; }
    OrderStatus _orderStatus;

    protected OrderTransactionBase(MarketBase market, Order order)
        : base(order)
    {
        throw new NotImplementedException();
    }

    protected OrderTransactionBase(MarketBase market, Order order, OrderTransactionBase parent)
        : base(order)
    {
        Market = market;
        Children = new(_children);
        Parent = parent;
        if (Parent != null)
        {
            Parent._children.Add(this);
        }

        _orderStatus = new OrderStatus(order);
    }

    protected virtual void RaiseOrderEvent(OrderEventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void ChangeState(OrderTransactionState newState)
    {
        //Log.Trace($"Transaction status changed: {Order.OrderType} {State} -> {newState}");
        State = newState;
    }

    public virtual void Cancel()
    {
        throw new NotImplementedException();
    }

    public virtual void EscalteEvent(OrderTransactionBase tx, OrderEventArgs e)
    {
    }

    public virtual void ProcessEvent(OrderEventType eventType)
    {
        DispatchEvent(eventType);
        if (Parent == null)
        {
            return;
        }
        Parent.EscalteEvent(this, new OrderEventArgs(eventType, DateTime.UtcNow, Order, this));
    }

    public virtual void DispatchEvent(OrderEventType eventType)
    {
        throw new NotImplementedException();
        //_market.InvokeOrderTransactionChanged(this, new OrderTransactionEventArgs(DateTime.UtcNow, eventType, this));
    }
}
