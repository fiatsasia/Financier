//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financial.Extensions
{
    public class TradingMarketModel<TPrice, TSize> : ITradingMarket<TPrice, TSize>
    {
        public string MarketSymbol { get; private set; }
        public ITradingAccount<TPrice, TSize> TradingAccount { get; private set; }

        public virtual TPrice BestBidPrice { get; private set; }
        public virtual TSize BestBidSize { get; private set; }
        public virtual TPrice BestAskPrice { get; private set; }
        public virtual TSize BestAskSize { get; private set; }

        TradingOrderFactoryModel _orderFactory;
        public TradingOrderFactoryBase GetTradeOrderFactory() => _orderFactory;

        List<ITradingOrder> _orders = new List<ITradingOrder>();
        public IEnumerable<ITradingOrder> ListOrders() => _orders;

        Dictionary<string, ITradingPosition<TPrice, TSize>> _positions = new Dictionary<string, ITradingPosition<TPrice, TSize>>();
        public IEnumerable<ITradingPosition<TPrice, TSize>> ListPositions() => _positions.Values;

        public event Action<ITradingOrder> OrderChanged;
        public event Action<ITradingPosition<TPrice, TSize>> PositionChanged;

        public TradingMarketModel(ITradingAccount<TPrice, TSize> account, string marketSymbol)
        {
            TradingAccount = account;
            MarketSymbol = marketSymbol;
        }

        public virtual Task PlaceOrder(ITradingOrder order)
        {
            return Task.Run(() =>
            {
                //
                // ここで APIで注文を送信する。
                //
                _orders.Add(order);
                return;
            });
        }

        void OnOrderChanged(ITradingSimpleOrder order)
        {
            // 画面表示などにつなげる
        }

        void OnPositionChanged(ITradingPosition<TPrice, TSize> pos)
        {
            // 画面表示などにつなげる
        }
    }
}
