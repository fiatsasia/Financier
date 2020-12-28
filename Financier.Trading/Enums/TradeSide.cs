//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
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
