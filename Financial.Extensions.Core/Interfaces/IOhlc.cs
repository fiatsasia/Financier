//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public interface IOhlc<TPrice> where TPrice : IComparable
    {
        DateTime Start { get; }
        TPrice Open { get; }
        TPrice High { get; }
        TPrice Low { get; }
        TPrice Close { get; }
    }

    public interface IOhlcv<T> : IOhlc<T> where T : IComparable
    {
        double Volume { get; }
    }

    public interface IOhlcvv<T> : IOhlcv<T> where T : IComparable
    {
        double VWAP { get; }
    }
}
