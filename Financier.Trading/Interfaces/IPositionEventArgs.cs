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
    public interface IPositionEventArgs
    {
        PositionEventType EventType { get; }
        Ulid Id { get; }
        Ulid OrderId { get; }

        bool IsOpened { get; }
        decimal Size { get; }
        DateTime OpenTime { get; }
        decimal OpenPrice { get; }
        DateTime? CloseTime { get; }
        decimal? ClosePrice { get; }
        decimal TotalOpenSize { get; }
        decimal? Profit { get; }
        decimal? NetProfit { get; }
    }
}
