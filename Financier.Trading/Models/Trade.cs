//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    class Trade : ITrade
    {
        public DateTime OpenTime { get; protected set; } = DateTime.MinValue;
        public decimal OpenPrice { get; protected set; }
        public DateTime CloseTime { get; protected set; } = DateTime.MaxValue;
        public decimal ClosePrice { get; protected set; }
        public decimal TradeSize { get; protected set; }
        public PositionState Status { get; private set; }
        public bool IsOpened => OpenTime > DateTime.MinValue && CloseTime == DateTime.MaxValue;
        public bool IsClosed => CloseTime < DateTime.MaxValue;

        IMarket _market;

        public Trade(IMarket market)
        {
            _market = market;
        }

        public decimal UnrealizedProfit => IsOpened ? CalculateProfit(OpenPrice, _market.LastTradedPrice, TradeSize) : decimal.Zero;
        public decimal RealizedProfit => IsClosed ? CalculateProfit(OpenPrice, ClosePrice, TradeSize) : decimal.Zero;

        decimal CalculateProfit(decimal openPrice, decimal closePrice, decimal size)
        {
            return Calculator.ToDecimal(Calculator.Sub(closePrice, openPrice)) * Calculator.ToDecimal(size);
        }

        public virtual void Open(DateTime time, decimal openPrice, decimal size)
        {
            OpenTime = time;
            OpenPrice = openPrice;
            TradeSize = size;
            Status = PositionState.Active;
        }

        public virtual void Close(DateTime time, decimal closePrice)
        {
            CloseTime = time;
            ClosePrice = closePrice;
            Status = PositionState.Closed;
        }
    }
}
