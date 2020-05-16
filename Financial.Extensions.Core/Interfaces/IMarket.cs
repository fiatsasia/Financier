//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions.Trading
{
    // - MarketConfigを追加
    //      - 両建て不可
    //          - 反対売買は建玉相殺
    //      - 売り建て不可
    //      - ポジション合算
    public interface IMarketConfig
    {
    }

    public interface IMarket
    {
        DateTime LastUpdatedTime { get; }

        bool HasActiveOrder { get; }
    }

    public interface IMarket<TPrice, TSize> : IMarket
    {
        // Precision (price and size)
        // Number of decimal places (price and size)
        TPrice MarketPrice { get; }

        bool PlaceOrder(IOrder<TPrice, TSize> order);
        bool PlaceOrder(IOrder<TPrice, TSize> order, TimeInForce tif);
        IOrderFactory<TPrice, TSize> GetOrderFactory();

        void UpdatePrice(DateTime time, TPrice price);

        event Action<IOrder<TPrice, TSize>> OrderChanged;
    }
}
