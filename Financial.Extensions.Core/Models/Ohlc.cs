//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Collections.Generic;

namespace Financial.Extensions
{
    // OHLC + Start
    public class Ohlc<TPrice> : IOhlc<TPrice>
    {
        protected TimeSpan Period { get; private set; }

        DateTime _start = DateTime.MaxValue;
        DateTime _end = DateTime.MinValue;
        public DateTime Start
        {
            get => _start.Round(Period);
            protected set { _start = value; }
        }

        public TPrice Open { get; protected set; }
        public TPrice High { get; protected set; }
        public TPrice Low { get; protected set; }
        public TPrice Close { get; protected set; }

        public Ohlc(TimeSpan period)
        {
            Period = period;
            High = Calculator.MinValue<TPrice>();
            Low = Calculator.MaxValue<TPrice>();
        }

        public virtual void Update(DateTime time, TPrice price)
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

            if (Calculator.CompareTo(price, High) > 0) High = price;
            if (Calculator.CompareTo(price, Low) < 0) Low = price;
        }

        public virtual double GetTypicalPrice(TypicalPriceKind kind)
        {
            switch (kind)
            {
                case TypicalPriceKind.Close:
                    return Calculator.ToDouble(Close);

                case TypicalPriceKind.TypicalPrice:
                    return Calculator.ToDouble(new TPrice[] { High, Low, Close }.Average());

                case TypicalPriceKind.OHLC:
                    return Calculator.ToDouble(new TPrice[] { Open, High, Low, Close }.Average());

                case TypicalPriceKind.HLCC:
                    return Calculator.ToDouble(new TPrice[] { High, Low, Close, Close }.Average());

                case TypicalPriceKind.HLOO:
                    return Calculator.ToDouble(new TPrice[] { Open, Open, High, Low }.Average());

                default:
                    throw new InvalidOperationException();
            }
        }
    }

    // OHLC + Start + Volume
    public class Ohlcv<TPrice> : Ohlc<TPrice>, IOhlcv<TPrice>
    {
        public double Volume { get; protected set; }

        public Ohlcv(TimeSpan period) : base(period) { }

        public Ohlcv(TimeSpan period, DateTime start, TPrice open, TPrice high, TPrice low, TPrice close, double volume)
            : base(period)
        {
            Start = start;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }

        public virtual void Update<TSize>(DateTime time, TPrice price, TSize size)
        {
            base.Update(time, price);
            Volume += Math.Abs(Calculator.ToDouble(size)); // Some of system indicates sell as minus
        }
    }

    // OHLC + Start + Volume + VWAP(Volume Weighted Average Price)
    public class Ohlcvv<TPrice> : Ohlcv<TPrice>, IOhlcvv<TPrice>
    {
        decimal _amount;

        public double VWAP { get; protected set; }
        public virtual double TypicalPrice { get { return VWAP; } }

        public Ohlcvv(TimeSpan period) : base(period) { }

        public Ohlcvv(TimeSpan period, DateTime start, TPrice open, TPrice high, TPrice low, TPrice close, double volume, double vwap)
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

        public Ohlcvv(TimeSpan period, IEnumerable<IOhlcvv<TPrice>> ohlcs)
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

        public override void Update<TSize>(DateTime time, TPrice price, TSize size)
        {
            base.Update(time, price, size);
            _amount += Calculator.ToDecimal(price) * Math.Abs(Calculator.ToDecimal(size));
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
