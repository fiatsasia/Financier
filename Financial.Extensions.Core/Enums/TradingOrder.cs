//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financial.Extensions
{
    public enum TradingOrderType
    {
        Unspecified,

        // Simple order types
        LimitPrice,
        MarketPrice,

        // Conditioned order types
        Stop,
        StopLimit,
        TrailingStop,

        // Combined order types
        IFD,
        OCO,
        IFO,
        IFDOCO = IFO,
    }

    public enum TradeConditionalOrderType
    {
        IFD,
        OCO,
        IFO,
        IFDOCO = IFO,
        OSO,
    }

    public enum TradingOrderState
    {
        New,
        PartiallyFilled,
        Filled,
        Canceled,
        Rejected, // 注文を送信して、サイズ不正などで失敗した場合、IFDなどで後続注文が失敗した場合など
        Expired,
    }

    public enum TradeOrderTransactionState
    {
        Created,
        Accepted,
        Confirmed,
        Failed,
    }
}
