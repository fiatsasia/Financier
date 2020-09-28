//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financier.Trading
{
    public interface IOrderTransaction
    {
        string Id { get; }
        DateTime OpenTime { get; }
        OrderTransactionState State { get; }
        IOrderTransaction[] Children { get; }
        IOrderTransaction Parent { get; }

        void SetParent(IOrderTransaction parent);
        IOrderTransaction AddChild(IOrderTransaction child);

        void OnChildOrderTransactionChanged(IOrderTransaction tx, OrderTransactionEventArgs ev);
        event EventHandler<OrderTransactionEventArgs> OrderTransactionChanged;

        IOrder Order { get; }

        bool IsCancelable { get; }
        void Cancel();
    }
}
