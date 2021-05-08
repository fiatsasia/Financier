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
    public interface IOrderTransaction
    {
        Ulid Id { get; }
        DateTime OpenTime { get; }
        OrderTransactionState State { get; }
        IOrderTransaction[] Children { get; }
        IOrderTransaction Parent { get; }

        IOrderTransaction AddChild(IOrderTransaction child);
        void EscalteEvent(IOrderTransaction tx, IOrderTransactionEventArgs ev);

        IOrder Order { get; }
        bool IsCancelable { get; } // Will be used to enable/disable cancel buttons
        void Cancel();
    }
}
