//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Financial.Extensions
{
    public abstract class TradingPositionBase<TAmount, TSize> : ITradingPosition<TAmount, TSize>
    {
        public DateTime OpenTime { get; protected set; } = DateTime.MinValue;
        public TAmount OpenPrice { get; protected set; }
        public DateTime CloseTime { get; protected set; } = DateTime.MaxValue;
        public TAmount ClosePrice { get; protected set; }
        public TSize Size { get; protected set; }
        public TradePositionState Status { get; private set; }

        public virtual TAmount ProfitAmount => CalculateProfit(ClosePrice);
        public virtual double ProfitRate => CalculateProfitRate(ClosePrice);
        public bool Opened => OpenTime > DateTime.MinValue;

        public virtual void Open(DateTime time, TAmount openPrice, TSize size)
        {
            OpenTime = time;
            OpenPrice = openPrice;
            Size = size;
        }

        public virtual void Close(DateTime time, TAmount closePrice)
        {
            CloseTime = time;
            ClosePrice = closePrice;
        }

        public virtual TAmount CalculateProfit(TAmount currentPrice)
        {
            if (typeof(TAmount) == typeof(double) && typeof(TSize) == typeof(double))
            {
                var size = (double)(object)Size;
                var openPrice = (double)(object)OpenPrice;
                var cp = (double)(object)currentPrice;
                return (TAmount)(object)(((size < 0.0d) ? openPrice - cp : cp - openPrice) * size);
            }
            else if (typeof(TAmount) == typeof(decimal) && typeof(TSize) == typeof(decimal))
            {
                var size = (decimal)(object)Size;
                var openPrice = (decimal)(object)OpenPrice;
                var cp = (decimal)(object)currentPrice;
                return (TAmount)(object)(((size < 0.0m) ? openPrice - cp : cp - openPrice) * size);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public virtual double CalculateProfitRate(TAmount currentPrice)
        {
            if (typeof(TAmount) == typeof(double) && typeof(TSize) == typeof(double))
            {
                var size = (double)(object)Size;
                var openPrice = (double)(object)OpenPrice;
                var profit = (double)(object)CalculateProfit(currentPrice);
                return profit / (openPrice * size);
            }
            else if (typeof(TAmount) == typeof(decimal) && typeof(TSize) == typeof(decimal))
            {
                var size = (decimal)(object)Size;
                var openPrice = (decimal)(object)OpenPrice;
                var profit = (decimal)(object)CalculateProfit(currentPrice);
                return Convert.ToDouble(profit / (openPrice * size));
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
