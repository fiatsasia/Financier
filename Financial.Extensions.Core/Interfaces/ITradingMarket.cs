//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    // - MarketConfigを追加
    //      - 両建て不可
    //          - 反対売買は建玉相殺
    //      - 売り建て不可
    //      - ポジション合算
    public interface ITradingMarketConfig
    {
    }

    public interface ITradingMarket
    {
        DateTime LastUpdatedTime { get; }

        bool HasActiveOrder { get; }
        bool HasOpenPosition { get; }
    }

    public interface ITradingMarket<TPrice, TSize> : ITradingMarket
    {
        // Precision (price and size)
        // Number of decimal places (price and size)
        TPrice MarketPrice { get; }

        void PlaceOrder(ITradingOrder<TPrice, TSize> order);

        ITradingOrderFactory<TPrice, TSize> GetTradeOrderFactory();

        void UpdatePrice(DateTime time, TPrice price);

        event Action<ITradingPosition<TPrice, TSize>> PositionChanged;
    }
}
