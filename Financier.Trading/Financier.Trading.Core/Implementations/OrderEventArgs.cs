//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading;

public class OrderEventArgs : EventArgs
{
    public OrderEventType EventType { get; }
    public Order Order { get; }
    public OrderStatus Status { get; }
    public OrderExecution Execution { get; }

    public DateTime Time { get; }
    public DateTime? Expiration { get; }
    public decimal? Price { get; }

    public OrderEventArgs(OrderEventType eventType, DateTime time, Order order, OrderStatus status)
    {
        EventType = eventType;
        Time = time;
        Order = order;
        Status = status;
    }

    public OrderEventArgs(DateTime time, OrderEventType eventType, Order order, OrderExecution execution)
    {
        Time = time;
        Order = order;
        EventType = eventType;
        Execution = execution;
    }
}
