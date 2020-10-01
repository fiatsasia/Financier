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
        DateTime? OpenTime { get; }
        DateTime? CloseTime { get; }

        OrderType OrderType { get; }
        decimal? OrderSize { get; }
        decimal? OrderPrice { get; }

        decimal? TriggerPrice { get; }      // stop loss, stop limit
        decimal? TrailingOffset { get; }    // trailing stop, trailing stop limit
        decimal? ProfitPrice { get; }       // Take profit

        OrderState State { get; }

        IEnumerable<IExecution> Executions { get; }
        decimal? ExecutedPrice { get; }
        decimal? ExecutedSize { get; }

        IReadOnlyList<IOrder> Children { get; }
    }
}
