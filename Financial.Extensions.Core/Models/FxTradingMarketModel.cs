//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financial.Extensions
{
    public class FxTradingMarketModel : IFxTradingMarket
    {
        public string MarketSymbol { get; private set; }
        public IFxTradingAccount TradingAccount { get; private set; }

        public virtual decimal BestBidPrice { get; private set; }
        public virtual decimal BestBidSize { get; private set; }
        public virtual decimal BestAskPrice { get; private set; }
        public virtual decimal BestAskSize { get; private set; }

        FxTradingOrderFactoryModel _orderFactory;
        public FxTradingOrderFactoryBase GetTradeOrderFactory() => _orderFactory;

        Dictionary<Guid, IFxTradingOrder> _orders = new Dictionary<Guid, IFxTradingOrder>();
        public IEnumerable<IFxTradingOrder> ListOrders() => _orders.Values;

        Dictionary<string, IFxTradingPosition> _positions = new Dictionary<string, IFxTradingPosition>();
        public IEnumerable<IFxTradingPosition> ListPositions() => _positions.Values;

        public event Action<IFxTradingOrder> OrderChanged;
        public event Action<IFxTradingPosition> PositionChanged;

        public FxTradingMarketModel(IFxTradingAccount account, string marketSymbol)
        {
            TradingAccount = account;
            MarketSymbol = marketSymbol;
        }

        public virtual Task PlaceOrder(IFxTradingOrder order)
        {
            return Task.Run(() =>
            {
                //
                // ここで APIで注文を送信する。
                //
                _orders.Add(order.OrderId, order);
                return;
            });
        }

        void OnOrderChanged(IFxTradingSimpleOrder order)
        {
            // 画面表示などにつなげる
        }

        void OnPositionChanged(IFxTradingPosition pos)
        {
            // 画面表示などにつなげる
        }
    }
}
