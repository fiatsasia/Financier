//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financier.Trading
{
    public enum OrderType
    {
        Unspecified,

        // Simple order types
        LimitPrice,
        MarketPrice,

        // Conditioned order types
        Stop,
        StopLimit,
        TrailingStop,

        // Structured
        StopAndReverse,

        // Combined order types
        IFD,
        OCO,
        IFO,
        IFDOCO = IFO,
        OSO,
    }

    public static class OrderTypeExtension
    {
        public static bool IsSimpleOrder(this OrderType orderType)
        {
            switch (orderType)
            {
                case OrderType.LimitPrice:
                case OrderType.MarketPrice:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsCombinedOrder(this OrderType orderType)
        {
            switch (orderType)
            {
                case OrderType.IFD:
                case OrderType.OCO:
                case OrderType.IFO:
                    return true;

                default:
                    return false;
            }
        }
    }

    public enum OrderState
    {
        New,
        PartiallyFilled,
        Filled,
        Canceled,
        Rejected, // 注文を送信して、サイズ不正などで失敗した場合、IFDなどで後続注文が失敗した場合など
        Expired,
    }
}
