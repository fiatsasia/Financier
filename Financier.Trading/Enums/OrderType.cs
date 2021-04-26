//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading
{
    public enum OrderType
    {
        Unknown = -1,

        Unspecified,
        NullOrder,

        // Simple order types
        Market,
        Limit,

        // Conditioned order types
        Stop,
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
            OrderType.Limit => true,
            OrderType.Market => true,
            _ => false
        };

        // OCO is not conditional
        public static bool IsConditionalOrder(this OrderType orderType) => orderType switch
        {
            OrderType.Stop => true,
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
            OrderType.Stop => true,
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
