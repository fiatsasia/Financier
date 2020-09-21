//==============================================================================
// Copyright (c) 2017-2020 Fiats Inc. All rights reserved.
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
        StopLoss,
        StopLossLimit,
        TrailingStop,
        TrailingStopLimit,

        // Structured
        StopAndReverse,

        // Combined order types
        IFD,
        OCO,
        IFO,
        IFDOCO = IFO,
        OSO,

        TriggerPriceBelow,
        TriggerPriceAbove,
        TriggerEvent,
        TriggerProfitAndLoss,
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
}
