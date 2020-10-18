//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public interface IOrderTransaction
    {
        string Id { get; }
        DateTime OpenTime { get; }
        OrderTransactionState State { get; }
        IOrderTransaction[] Children { get; }
        IOrderTransaction Parent { get; }

        IOrderTransaction AddChild(IOrderTransaction child);

        void EscalteEvent(IOrderTransaction tx, OrderTransactionEventArgs ev);

        IOrder Order { get; }

        bool IsCancelable { get; } // Will be used to enable/disable cancel buttons
        void Cancel();
    }
}
