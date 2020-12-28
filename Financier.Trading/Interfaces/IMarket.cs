//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;

namespace Financier.Trading
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
        string MarketCode { get; }
        decimal MinimumOrderSize { get; }

        decimal BestAskPrice { get; }
        decimal BestBidPrice { get; }
        decimal MidPrice { get; }
        decimal LastTradedPrice { get; }

        event EventHandler<PositionEventArgs> PositionChanged;
        event EventHandler<OrderTransactionEventArgs> OrderTransactionChanged;

        void Open();
        bool HasActiveOrder { get; }
        IPositions Positions { get; }
        IObservable<ITicker> GetTickerSource();

        IOrderTransaction PlaceOrder(IOrderRequest request);
        IOrderTransaction PlaceOrder(IOrderRequest request, TimeInForce tif, DateTime validityPeriod, DateTime startTime);
    }
}
