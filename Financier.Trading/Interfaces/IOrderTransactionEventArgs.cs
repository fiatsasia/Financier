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
    public interface IOrderTransactionEventArgs
    {
        DateTime Time { get; }
        OrderTransactionEventType EventType { get; }
        Ulid OrderRequestId { get; }
        Ulid TransactionId { get; }
        Ulid OrderId { get; }
        OrderType OrderType { get; }
        decimal? OrderPrice { get; }
        decimal? OrderSize { get; }
        decimal? ExecutedPrice { get; }
        decimal? ExecutedSize { get; }
        decimal? TriggerPrice { get; }
    }
}
