//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Financial.Extensions
{
    public interface ITradingExecution<TAmount, TSize>
    {
        DateTime Time { get; }
        TAmount Price { get; }
        TSize Size { get; }
    }

    public interface ITradingExecutions<TAmount, TSize>
    {
        TAmount AveragePrice { get; }
        TSize TotalSize { get; }

        void Executed(TAmount executedPrice, TSize executedSize);
    }
}
