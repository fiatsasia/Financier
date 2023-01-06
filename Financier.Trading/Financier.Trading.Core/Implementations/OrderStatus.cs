//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading;

public class OrderStatus
{
    protected Order Order { get; }
    public IReadOnlyCollection<OrderExecution> Executions { get; }
    public decimal? ExecutedPrice { get; private set; }
    public decimal? ExecutedSize => Order.OrderSize.HasValue ? _executions.Sum(e => e.Price) : null;

    public virtual DateTime OrderTime { get; private set; }
    public virtual DateTime OpenTime { get; private set; }
    public virtual DateTime CloseTime { get; private set; }
    public DateTime LastUpdated { get; private set; }


    public DateTime Expiration { get; private set; }
    public decimal? TriggerPrice { get; private set; } 
    public DateTime? TriggerTime { get; private set; }
    public OrderState OrderState { get; private set; }

    List<OrderExecution> _executions = new();

    public OrderStatus(Order order)
    {
        Order = order;
        Executions = new ReadOnlyCollection<OrderExecution>(_executions);
    }

    public void OnOrderEvent(OrderEventArgs e)
    {
        LastUpdated = e.Time;

        switch (e.EventType)
        {
            case OrderEventType.Ordered:
                OrderTime = e.Time;
                OrderState = OrderState.Ordered;
                break;

            case OrderEventType.OrderOpened:
                OpenTime = e.Time;
                Expiration = e.Expiration.Value;
                OrderState = OrderState.OrderOpened;
                break;

            case OrderEventType.Triggered:
                TriggerTime = e.Time;
                TriggerPrice = e.Price;
                OrderState = OrderState.Triggered;
                break;

            case OrderEventType.Executed:
                _executions.Add(e.Execution);
                OrderState = (ExecutedSize.Value < Order.OrderSize) ? OrderState.PartiallyExecuted : OrderState.Executed;
                break;

            case OrderEventType.Completed:
                CloseTime = e.Time;
                OrderState = OrderState.Completed;
                break;

            default:
                throw new NotImplementedException();
        }
    }

    public virtual bool IsCancelable => throw new NotImplementedException();
}
