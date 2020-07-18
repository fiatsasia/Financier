//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    class Trade<TPrice, TSize> : ITrade<TPrice, TSize>
    {
        public DateTime OpenTime { get; protected set; } = DateTime.MinValue;
        public TPrice OpenPrice { get; protected set; }
        public DateTime CloseTime { get; protected set; } = DateTime.MaxValue;
        public TPrice ClosePrice { get; protected set; }
        public TSize TradeSize { get; protected set; }
        public PositionState Status { get; private set; }
        public bool IsOpened => OpenTime > DateTime.MinValue && CloseTime == DateTime.MaxValue;
        public bool IsClosed => CloseTime < DateTime.MaxValue;
        public TradeSide Side => Calculator.Sign(TradeSize) > 0 ? TradeSide.Buy : TradeSide.Sell;

        IMarket<TPrice, TSize> _market;

        public Trade(IMarket<TPrice, TSize> market)
        {
            _market = market;
        }

        public decimal UnrealizedProfit => IsOpened ? CalculateProfit(OpenPrice, _market.MarketPrice, TradeSize) : decimal.Zero;
        public decimal RealizedProfit => IsClosed ? CalculateProfit(OpenPrice, ClosePrice, TradeSize) : decimal.Zero;

        decimal CalculateProfit(TPrice openPrice, TPrice closePrice, TSize size)
        {
            return Calculator.ToDecimal(Calculator.Sub(closePrice, openPrice)) * Calculator.ToDecimal(size);
        }

        public virtual void Open(DateTime time, TPrice openPrice, TSize size)
        {
            OpenTime = time;
            OpenPrice = openPrice;
            TradeSize = size;
            Status = PositionState.Active;
        }

        public virtual void Close(DateTime time, TPrice closePrice)
        {
            CloseTime = time;
            ClosePrice = closePrice;
            Status = PositionState.Closed;
        }
    }
}
