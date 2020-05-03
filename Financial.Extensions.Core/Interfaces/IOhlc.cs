//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public interface IOhlc<TPrice>
    {
        DateTime Start { get; }
        TPrice Open { get; }
        TPrice High { get; }
        TPrice Low { get; }
        TPrice Close { get; }
    }

    public interface IOhlcv<TPrice> : IOhlc<TPrice>
    {
        double Volume { get; }
    }

    public interface IOhlcvv<TPrice> : IOhlcv<TPrice>
    {
        double VWAP { get; }
    }

    public enum OhlcSpanKind
    {
        TimePeriod,
        Ticks,
        Volume,
    }
}
