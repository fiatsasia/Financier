//==============================================================================
// Copyright (c) 2013-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    // OHLC + Start
    public class FxOhlcModel : IFxOhlc
    {
        protected TimeSpan Period { get; private set; }

        DateTime _start = DateTime.MaxValue;
        DateTime _end = DateTime.MinValue;
        public DateTime Start => _start.Round(Period);

        public decimal Open { get; private set; }
        public decimal High { get; private set; } = decimal.MinValue;
        public decimal Low { get; private set; } = decimal.MaxValue;
        public decimal Close { get; private set; }

        public FxOhlcModel(TimeSpan period)
        {
            Period = period;
        }

        public virtual void Update(DateTime time, decimal price)
        {
            if (time < _start)
            {
                _start = time;
                Open = price;
            }
            if (time > _end)
            {
                _end = time;
                Close = price;
            }

            High = Math.Max(High, price);
            Low = Math.Min(Low, price);
        }
    }

    // OHLC + Start + Volume
    public class FxOhlcvModel : FxOhlcModel, IFxOhlcv
    {
        public double Volume { get; private set; }

        public FxOhlcvModel(TimeSpan period) : base(period) { }

        public virtual void Update(DateTime time, decimal price, decimal size)
        {
            base.Update(time, price);
            Volume += unchecked((double)Math.Abs(size)); // Some of system indicates sell as minus
        }
    }

    // OHLC + Start + Volume + VWAP(Volume Weighted Average Price)
    public class FxOhlcvvModel : FxOhlcvModel, IFxOhlcvv
    {
        decimal _amount;

        public double VWAP { get; private set; }
        public virtual double TypicalPrice { get { return VWAP; } }

        public FxOhlcvvModel(TimeSpan period) : base(period) { }

        public override void Update(DateTime time, decimal price, decimal size)
        {
            base.Update(time, price, size);
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
    }
}
