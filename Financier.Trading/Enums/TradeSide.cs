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
        public static TradeSide Reverse(this TradeSide side)
        {
            switch (side)
            {
                case TradeSide.Buy: return TradeSide.Sell;
                case TradeSide.Sell: return TradeSide.Buy;
                default: throw new ArgumentException();
            }
        }
    }
}
