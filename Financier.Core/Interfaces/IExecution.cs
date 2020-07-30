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
        decimal Price { get; }
        decimal Size { get; }
    }

    public interface IExecutions
    {
        decimal AveragePrice { get; }
        decimal TotalSize { get; }

        void Executed(decimal executedPrice, decimal executedSize);
    }
}
