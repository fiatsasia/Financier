﻿//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading
{
    public enum PriceType
    {
        Nothing,
        Limit,
        Market,
        BestAsk,
        BestBid,
        Best,
        Mid,
        LastTraded,
        LastSold,
        LastBought,
        Executed,
    }

    public static class OrderPriceTypeExtension
    {
        public static bool IsNeedApplyPrice(this PriceType priceType) => priceType switch
        {
            PriceType.BestAsk => true,
            PriceType.BestBid => true,
            PriceType.Best => true,
            PriceType.Mid => true,
            PriceType.LastTraded => true,
            PriceType.LastSold => true,
            PriceType.LastBought => true,
            _ => false
        };
    }
}
