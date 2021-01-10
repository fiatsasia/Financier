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
    public interface IPosition
    {
        decimal Size { get; }

        bool IsOpened { get; }
        DateTime Open { get; }
        DateTime? Close { get; }
        decimal OpenPrice { get; }
        decimal? ClosePrice { get; }

        decimal? Profit { get; }
        decimal? NetProfit { get; }

        IPositions Positions { get; }
    }
}
