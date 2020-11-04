//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Collections.Generic;

namespace Financier
{
    // OHLC + Start
    public class Ohlc : IOhlc
    {
        protected TimeSpan Period { get; private set; }

        DateTime _start = DateTime.MaxValue;
        DateTime _end = DateTime.MinValue;
        public DateTime Start
        {
            get => _start.Round(Period);
            protected set { _start = value; }
        }

        public decimal Open { get; protected set; }
        public decimal High { get; protected set; }
        public decimal Low { get; protected set; }
        public decimal Close { get; protected set; }

        public Ohlc(TimeSpan period)
        {
            Period = period;
            High = decimal.MinValue;
            Low = decimal.MaxValue;
        }

        public virtual void Reset()
        {
            _start = DateTime.MaxValue;
            _end = DateTime.MinValue;
            Open = decimal.Zero;
            High = decimal.MinValue;
            Low = decimal.MaxValue;
            Close = decimal.Zero;
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

            High = Math.Max(price, High);
            Low = Math.Min(price, Low);
        }

        public virtual double GetTypicalPrice(TypicalPriceKind kind)
        {
            switch (kind)
            {
                case TypicalPriceKind.Close:
                    return Convert.ToDouble(Close);

                case TypicalPriceKind.TypicalPrice:
                    return Convert.ToDouble(new decimal[] { High, Low, Close }.Average());

                case TypicalPriceKind.OHLC:
                    return Convert.ToDouble(new decimal[] { Open, High, Low, Close }.Average());

                case TypicalPriceKind.HLCC:
                    return Convert.ToDouble(new decimal[] { High, Low, Close, Close }.Average());

                case TypicalPriceKind.HLOO:
                    return Convert.ToDouble(new decimal[] { Open, Open, High, Low }.Average());

                default:
                    throw new InvalidOperationException();
            }
        }
    }

    // OHLC + Start + Volume
    public class Ohlcv : Ohlc, IOhlcv
    {
        public double Volume { get; protected set; }

        public Ohlcv(TimeSpan period) : base(period) { }

        public Ohlcv(TimeSpan period, DateTime start, decimal open, decimal high, decimal low, decimal close, double volume)
            : base(period)
        {
            Start = start;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }

        public override void Reset()
        {
            base.Reset();
            Volume = 0d;
        }

        public virtual void Update(DateTime time, decimal price, decimal size)
        {
            base.Update(time, price);
            Volume += Math.Abs(Convert.ToDouble(size)); // Some of system indicates sell as minus
        }
    }

    // OHLC + Start + Volume + VWAP(Volume Weighted Average Price)
    public class Ohlcvv : Ohlcv, IOhlcvv
    {
        decimal _amount;

        public double VWAP { get; protected set; }
        public virtual double TypicalPrice { get { return VWAP; } }

        public Ohlcvv(TimeSpan period) : base(period) { }

        public Ohlcvv(TimeSpan period, DateTime start, decimal open, decimal high, decimal low, decimal close, double volume, double vwap)
            : base(period)
        {
            Start = start;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
            VWAP = vwap;
        }

        public Ohlcvv(TimeSpan period, IEnumerable<IOhlcvv> ohlcs)
            : base(period)
        {
            var first = ohlcs.First();
            Start = first.Start;
            Open = first.Open;
            High = ohlcs.Max(e => e.High);
            Low = ohlcs.Min(e => e.Low);
            Close = ohlcs.Last().Close;
            Volume = ohlcs.Sum(e => e.Volume);
            VWAP = ohlcs.Sum(e => e.VWAP * e.Volume) / Volume;
        }

        public override void Reset()
        {
            base.Reset();
            _amount = decimal.Zero;
            VWAP = 0d;
        }

        public override void Update(DateTime time, decimal price, decimal size)
        {
            base.Update(time, price, size);
            _amount += price * Math.Abs(size);
            try
            {
                VWAP = Convert.ToDouble(_amount / Convert.ToDecimal(Volume));
            }
            catch (DivideByZeroException)
            {
                VWAP = 0;
            }
        }

        public override double GetTypicalPrice(TypicalPriceKind kind)
        {
            switch (kind)
            {
                case TypicalPriceKind.VWAP:
                    return VWAP;

                default:
                    return base.GetTypicalPrice(kind);
            }
        }
    }
}
