//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public interface ITradingExecution
    {
        DateTime Time { get; }
    }

    public interface ITradingExecution<TPrice, TSize> : ITradingExecution
    {
        TPrice Price { get; }
        TSize Size { get; }
    }

    public interface ITradingExecutions
    {
    }

    public interface ITradingExecutions<TPrice, TSize> : ITradingExecutions
    {
        TPrice AveragePrice { get; }
        TSize TotalSize { get; }

        void Executed(TPrice executedPrice, TSize executedSize);
    }
}
