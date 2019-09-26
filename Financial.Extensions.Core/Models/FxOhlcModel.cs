//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Financial.Extensions
{
    public class FxOhlcModel : IFxOhlcvv
    {
        public TimeSpan Period { get; private set; }
        DateTime _start = DateTime.MaxValue;
        public DateTime Start => _start.Round(Period);
        public decimal Open { get; private set; }
        public decimal High { get; private set; } = decimal.MinValue;
        public decimal Low { get; private set; } = decimal.MaxValue;
        public decimal Close { get; private set; }
        public double Volume { get; private set; }
        public double VWAP { get; private set; }

        public DateTime End { get; private set; } = DateTime.MinValue;
        public virtual double TypicalPrice { get { return VWAP; } }

        decimal _amount;

        public FxOhlcModel(TimeSpan period)
        {
            Period = period;
        }

        public FxOhlcModel(IFxTradeStream trade, TimeSpan period)
        {
            Period = period;
            Update(trade);
        }

        public FxOhlcModel(IEnumerable<IFxTradeStream> trades, TimeSpan period)
        {
            Period = period;
            trades.ForEach(trade => Update(trade));
        }

        public virtual void Update(DateTime time, decimal price, decimal size)
        {
            if (time < _start)
            {
                _start = time;
                Open = price;
            }
            if (time > End)
            {
                End = time;
                Close = price;
            }

            High = Math.Max(High, price);
            Low = Math.Min(Low, price);
            Volume += unchecked((double)Math.Abs(size));

            _amount += Convert.ToDecimal(price * Math.Abs(size));
            try
            {
                VWAP = Convert.ToDouble(_amount / Convert.ToDecimal(Volume));
            }
            catch (DivideByZeroException)
            {
                VWAP = 0;
            }
        }

        public virtual void Update(IFxTradeStream trade)
        {
            Debug.Assert(trade != null);
            Update(trade.Time, trade.Price, trade.Size);
        }
    }
}
