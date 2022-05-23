//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;

namespace Financier.Trading
{
    public class OrderPositionEventArgs : EventArgs
    {
        public PositionEventType EventType { get; }
        public DateTime Time { get; }

        public OrderPosition Position { get; }
        public OrderExecution Execution { get; }
        public Order Order { get; }
        public OrderStatus OrderStatus { get; }

        public OrderPositionEventArgs()
        {
        }

        public OrderPositionEventArgs(PositionEventType eventType, DateTime time, OrderPosition position, OrderStatus orderStatus)
        {
            EventType = eventType;
            Time = time;
            Position = position;
            OrderStatus = orderStatus;
        }
    }
}
