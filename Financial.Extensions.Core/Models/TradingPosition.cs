//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    class TradingPosition<TPrice, TSize> : ITradingPosition<TPrice, TSize>
    {
        public DateTime OpenTime { get; protected set; } = DateTime.MinValue;
        public TPrice OpenPrice { get; protected set; }
        public DateTime CloseTime { get; protected set; } = DateTime.MaxValue;
        public TPrice ClosePrice { get; protected set; }
        public TSize Size { get; protected set; }
        public TradePositionState Status { get; private set; }
        public bool IsOpened => OpenTime > DateTime.MinValue && CloseTime == DateTime.MaxValue;
        public bool IsClosed => CloseTime < DateTime.MaxValue;
        public TradeSide Side => Calculator.Sign(Size) > 0 ? TradeSide.Buy : TradeSide.Sell;

        ITradingMarket<TPrice, TSize> _market;

        public TradingPosition(ITradingMarket<TPrice, TSize> market)
        {
            _market = market;
        }

        public virtual ITradingOrder<TPrice, TSize> CreateSettlementMarketPriceOrder()
        {
            return _market.GetTradeOrderFactory().CreateSettlementMarketPriceOrder(this);
        }

        public virtual ITradingOrder<TPrice, TSize> CreateSettlementLimitPriceOrder(TPrice price)
        {
            return _market.GetTradeOrderFactory().CreateSettlementLimitPriceOrder(this, price);
        }

        public virtual ITradingOrder<TPrice, TSize> CreateStopAndReverseOrder()
        {
            return _market.GetTradeOrderFactory().CreateStopAndReverseOrder(this);
        }

        public decimal UnrealizedProfit => IsOpened ? CalculateProfit(OpenPrice, _market.MarketPrice, Size) : decimal.Zero;
        public decimal RealizedProfit => IsClosed ? CalculateProfit(OpenPrice, ClosePrice, Size) : decimal.Zero;

        decimal CalculateProfit(TPrice openPrice, TPrice closePrice, TSize size)
        {
            if (Calculator.Sign(size) < 0) // short position
            {
                return Calculator.ToDecimal(Calculator.Sub(openPrice, closePrice)) * Calculator.ToDecimal(size);
            }
            else // long position
            {
                return Calculator.ToDecimal(Calculator.Sub(closePrice, openPrice)) * Calculator.ToDecimal(size);
            }
        }

        public virtual void Open(DateTime time, TPrice openPrice, TSize size)
        {
            OpenTime = time;
            OpenPrice = openPrice;
            Size = size;
            Status = TradePositionState.Active;
        }

        public virtual void Close(DateTime time, TPrice closePrice)
        {
            CloseTime = time;
            ClosePrice = closePrice;
            Status = TradePositionState.Closed;
        }
    }
}
