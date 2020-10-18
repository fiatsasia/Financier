//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
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
        TOrderFactory GetOrderFactory<TOrderFactory>() where TOrderFactory : IOrderFactory;
        IObservable<ITicker> GetTickerSource();

        IOrderTransaction PlaceOrder(IOrder order);
        IOrderTransaction PlaceOrder(IOrder order, TimeSpan timeToExpire, TimeInForce timeInForce);
    }
}
