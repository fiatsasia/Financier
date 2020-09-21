//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
