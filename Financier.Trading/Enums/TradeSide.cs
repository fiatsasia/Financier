//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public enum TradeSide
    {
        NotSpecified,
        Buy,
        Sell,
    }

    public static class TradeSideExtension
    {
        public static TradeSide Reverse(this TradeSide side) => side switch
        {
            TradeSide.Buy => TradeSide.Sell,
            TradeSide.Sell => TradeSide.Buy,
            _ => throw new ArgumentException()
        };
    }
}
