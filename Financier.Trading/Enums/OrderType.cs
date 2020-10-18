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
        MarketPrice,
        LimitPrice,

        // Conditioned order types
        StopLoss,
        StopLimit,
        TrailingStop,
        TrailingStopLimit,
        TakeProfit,

        // Combined order types
        IFD,
        OCO,
        IFO,
        IFDOCO = IFO,
        OSO,

        // Fundamenal operations
        TriggerPriceBelow,
        TriggerPriceAbove,
        TriggerEvent,
        TriggerTrailingOffset,

        // Reserved
        Bracket,
        BracketOCO,
        BracketOSO,
        StopAndReverse,
    }

    public static class OrderTypeExtension
    {
        public static bool IsSimpleOrder(this OrderType orderType) => orderType switch
        {
            OrderType.LimitPrice => true,
            OrderType.MarketPrice => true,
            _ => false
        };

        // OCO is not conditional
        public static bool IsConditionalOrder(this OrderType orderType) => orderType switch
        {
            OrderType.StopLoss => true,
            OrderType.StopLimit => true,
            OrderType.TrailingStop => true,
            OrderType.TrailingStopLimit => true,
            OrderType.StopAndReverse => true,
            OrderType.TriggerPriceBelow => true,
            OrderType.TriggerPriceAbove => true,
            OrderType.TriggerTrailingOffset => true,
            OrderType.IFD => true,
            OrderType.IFDOCO => true,
            _ => false
        };

        public static bool IsCombinedOrder(this OrderType orderType) => orderType switch
        {
            OrderType.IFD => true,
            OrderType.OCO => true,
            OrderType.IFDOCO => true,
            _ => false
        };

        public static bool IsTriggerPrice(this OrderType orderType) => orderType switch
        {
            OrderType.StopLoss => true,
            OrderType.StopLimit => true,
            OrderType.TrailingStop => true,
            OrderType.TrailingStopLimit => true,
            OrderType.StopAndReverse => true,
            OrderType.TriggerPriceBelow => true,
            OrderType.TriggerPriceAbove => true,
            OrderType.TriggerTrailingOffset => true,
            _ => false
        };
    }
}
