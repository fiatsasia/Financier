//==============================================================================
// Copyright (c) 2017-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financier.Trading
{
    public enum OrderType
    {
        Unspecified,
        NullOrder,

        // Simple order types
        LimitPrice,
        MarketPrice,

        // Conditioned order types
        StopLoss,
        StopLimit,
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
        TriggerPriceOffset,
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

        // OCO is not conditional
        public static bool IsConditionalOrder(this OrderType orderType)
        {
            switch (orderType)
            {
                case OrderType.StopLoss:
                case OrderType.StopLimit:
                case OrderType.TrailingStop:
                case OrderType.TrailingStopLimit:
                case OrderType.StopAndReverse:
                case OrderType.TriggerPriceBelow:
                case OrderType.TriggerPriceAbove:
                case OrderType.TriggerPriceOffset:
                case OrderType.IFD:
                case OrderType.IFDOCO:
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
                case OrderType.IFDOCO:
                    return true;

                default:
                    return false;
            }
        }

        public static bool IsTriggerPrice(this OrderType orderType)
        {
            switch (orderType)
            {
                case OrderType.StopLoss:
                case OrderType.StopLimit:
                case OrderType.TrailingStop:
                case OrderType.TrailingStopLimit:
                case OrderType.StopAndReverse:
                case OrderType.TriggerPriceBelow:
                case OrderType.TriggerPriceAbove:
                case OrderType.TriggerPriceOffset:
                    return true;

                default:
                    return false;
            }
        }
    }
}
