//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier
{
    public interface IOhlc
    {
        DateTime Start { get; }
        decimal Open { get; }
        decimal High { get; }
        decimal Low { get; }
        decimal Close { get; }
    }

    public interface IOhlcv : IOhlc
    {
        double Volume { get; }
    }

    public interface IOhlcvv : IOhlcv
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
