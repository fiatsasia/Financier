//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public class FinancierEventArgs : EventArgs
    {
        public DateTime Time { get; set; }
    }

    public class PositionEventArgs : FinancierEventArgs
    {
        public PositionEventType EventType { get; set; }
        public IPosition Position { get; set; }
    }

    public class OrderTransactionEventArgs : FinancierEventArgs
    {
        public OrderTransactionEventType EventType { get; set; }
        public IMarket Market { get; set; }
        public IOrder Order { get; set; }
        public OrderTransactionEventType ChildEventType { get; set; }
        public IOrder ChildOrder { get; set; }
    }
}
