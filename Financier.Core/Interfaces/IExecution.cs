//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public interface IExecution
    {
        DateTime Time { get; }
    }

    public interface IExecution<TPrice, TSize> : IExecution
    {
        TPrice Price { get; }
        TSize Size { get; }
    }

    public interface IExecutions
    {
    }

    public interface IExecutions<TPrice, TSize> : IExecutions
    {
        TPrice AveragePrice { get; }
        TSize TotalSize { get; }

        void Executed(TPrice executedPrice, TSize executedSize);
    }
}
