//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier;

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
    decimal Volume { get; }
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
