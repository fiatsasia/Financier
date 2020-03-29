//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public enum TradeSide
    {
        Unspecified,
        Buy,
        Sell,
        BuySell,
    }

    public static class FxTradeSideExtension
    {
        public static TradeSide Opposite(this TradeSide side)
        {
            switch (side)
            {
                case TradeSide.Buy:
                    return TradeSide.Sell;

                case TradeSide.Sell:
                    return TradeSide.Buy;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
