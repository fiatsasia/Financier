//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;

namespace Financier.Trading
{
    public class FinancierEventArgs : EventArgs
    {
        public DateTime Time { get; }

        public FinancierEventArgs(DateTime time)
        {
            Time = time;
        }
    }

    public class PositionEventArgs : FinancierEventArgs
    {
        public PositionEventType EventType { get; }
        public IPosition Position { get; }

        public PositionEventArgs(DateTime time, PositionEventType eventType, IPosition position)
            : base(time)
        {
            EventType = eventType;
            Position = position;
        }
    }

    public class OrderTransactionEventArgs : FinancierEventArgs
    {
        public OrderTransactionEventType EventType { get; }
        public IMarket Market { get; }
        public IOrder Order { get; }

        public OrderTransactionEventArgs(DateTime time, OrderTransactionEventType eventType, IMarket market, IOrder order)
            : base(time)
        {
            EventType = eventType;
            Market = market;
            Order = order;
        }
    }
}
