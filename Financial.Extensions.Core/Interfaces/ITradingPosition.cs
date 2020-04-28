//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public interface ITradingPosition
    {
        DateTime OpenTime { get; }
        DateTime CloseTime { get; }

        TradePositionState Status { get; }
        TradeSide Side { get; }

        bool IsOpened { get; }
        bool IsClosed { get; }

        decimal UnrealizedProfit { get; }
        decimal RealizedProfit { get; }
    }

    public interface ITradingPosition<TPrice, TSize> : ITradingPosition
    {
        TPrice OpenPrice { get; }
        TPrice ClosePrice { get; }
        TSize Size { get; }

        void Open(DateTime time, TPrice openPrice, TSize size);
        void Close(DateTime time, TPrice closePrice);

        ITradingOrder<TPrice, TSize> CreateSettlementMarketPriceOrder();
        ITradingOrder<TPrice, TSize> CreateSettlementLimitPriceOrder(TPrice price);
        ITradingOrder<TPrice, TSize> CreateStopAndReverseOrder();
    }
}
