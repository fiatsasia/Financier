﻿//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public enum FxTradeSide
    {
        Unspecified,
        Buy,
        Sell,
        BuySell,
    }

    public static class FxTradeSideExtension
    {
        public static FxTradeSide Opposite(this FxTradeSide side)
        {
            switch (side)
            {
                case FxTradeSide.Buy:
                    return FxTradeSide.Sell;

                case FxTradeSide.Sell:
                    return FxTradeSide.Buy;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}