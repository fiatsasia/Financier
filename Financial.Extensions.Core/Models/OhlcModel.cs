﻿//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Collections.Generic;

namespace Financial.Extensions
{
    // OHLC + Start
    public class OhlcModel<TPrice> : IOhlc<TPrice> where TPrice : IComparable
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

        public OhlcModel(TimeSpan period)
        {
            Period = period;

            switch (typeof(TPrice))
            {
                case Type f when f == typeof(float):
                    High = (TPrice)(object)float.MinValue;
                    Low = (TPrice)(object)float.MaxValue;
                    break;

                case Type dbl when dbl == typeof(double):
                    High = (TPrice)(object)double.MinValue;
                    Low = (TPrice)(object)double.MaxValue;
                    break;

                case Type dec when dec == typeof(decimal):
                    High = (TPrice)(object)decimal.MinValue;
                    Low = (TPrice)(object)decimal.MaxValue;
                    break;

                default:
                    throw new NotSupportedException($"Not supported type '{typeof(TPrice).Name}'");
            }
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

            if (price.CompareTo(High) > 0) High = price;
            if (price.CompareTo(Low) < 0) Low = price;
        }
    }

    // OHLC + Start + Volume
    public class OhlcvModel<TPrice> : OhlcModel<TPrice>, IOhlcv<TPrice> where TPrice : IComparable
    {
        public double Volume { get; protected set; }

        public OhlcvModel(TimeSpan period) : base(period) { }

        public virtual void Update(DateTime time, TPrice price, float size)
        {
            base.Update(time, price);
            Volume += Math.Abs(size); // Some of system indicates sell as minus
        }

        public virtual void Update(DateTime time, TPrice price, double size)
        {
            base.Update(time, price);
            Volume += Math.Abs(size); // Some of system indicates sell as minus
        }

        public virtual void Update(DateTime time, TPrice price, decimal size)
        {
            base.Update(time, price);
            Volume += unchecked((double)Math.Abs(size)); // Some of system indicates sell as minus
        }
    }

    // OHLC + Start + Volume + VWAP(Volume Weighted Average Price)
    public class OhlcvvModel<TPrice> : OhlcvModel<TPrice>, IOhlcvv<TPrice> where TPrice : IComparable
    {
        decimal _amount;

        public double VWAP { get; protected set; }
        public virtual double TypicalPrice { get { return VWAP; } }

        public OhlcvvModel(TimeSpan period) : base(period) { }

        public OhlcvvModel(TimeSpan period, DateTime start, TPrice open, TPrice high, TPrice low, TPrice close, double volume, double vwap)
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

        public OhlcvvModel(TimeSpan period, IEnumerable<IOhlcvv<TPrice>> ohlcs)
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

        public override void Update(DateTime time, TPrice price, float size)
        {
            base.Update(time, price, size);
            _amount += (decimal)(object)price * Convert.ToDecimal(Math.Abs(size));
            try
            {
                VWAP = Convert.ToDouble(_amount / Convert.ToDecimal(Volume));
            }
            catch (DivideByZeroException)
            {
                VWAP = 0;
            }
        }

        public override void Update(DateTime time, TPrice price, double size)
        {
            base.Update(time, price, size);
            _amount += (decimal)(object)price * Convert.ToDecimal(Math.Abs(size));
            try
            {
                VWAP = Convert.ToDouble(_amount / Convert.ToDecimal(Volume));
            }
            catch (DivideByZeroException)
            {
                VWAP = 0;
            }
        }

        public override void Update(DateTime time, TPrice price, decimal size)
        {
            base.Update(time, price, size);
            _amount += (decimal)(object)price * Math.Abs(size);
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