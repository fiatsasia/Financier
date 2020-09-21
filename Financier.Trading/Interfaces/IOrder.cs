//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financier.Trading
{
    public interface IOrder
    {
        OrderType OrderType { get; }
        decimal? OrderPrice { get; }
        decimal? OrderSize { get; }

        decimal? TriggerPrice { get; }
        decimal? TrailingOffset { get; }

        DateTime? OpenTime { get; }
        DateTime? CloseTime { get; }
        OrderState State { get; }

        IEnumerable<IExecution> Executions { get; }
        decimal? ExecutedPrice { get; }
        decimal? ExecutedSize { get; }

        IReadOnlyList<IOrder> Children { get; }

        OrderPriceType OrderPriceType { get; }
        decimal OrderPriceOffset { get; }
        OrderPriceType TriggerPriceType { get; }
        decimal TriggerPriceOffset { get; }
        OrderPriceType ReferencePriceType { get; }
        OrderTransactionEventType TriggerEventType { get; }
    }
}
