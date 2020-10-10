//==============================================================================
// Copyright (c) 2017-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financier.Trading
{
    public enum OrderPriceType
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
    }

    public static class OrderPriceTypeExtension
    {
        public static bool IsNeedApplyPrice(this OrderPriceType priceType)
        {
            switch (priceType)
            {
                case OrderPriceType.BestAsk:
                case OrderPriceType.BestBid:
                case OrderPriceType.Best:
                case OrderPriceType.Mid:
                case OrderPriceType.LastTraded:
                case OrderPriceType.LastSold:
                case OrderPriceType.LastBought:
                    return true;

                default:
                    return false;
            }
        }
    }
}
