//==============================================================================
// Copyright (c) 2017-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
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
