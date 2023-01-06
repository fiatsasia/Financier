//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier;

// OHLC + Start
public class Ohlc : IOhlc
{
    protected TimeSpan FrameSpan { get; private set; }

    DateTime _start = DateTime.MaxValue;
    DateTime _end = DateTime.MinValue;
    public DateTime Start
    {
        get => _start.Round(FrameSpan);
        protected set { _start = value; }
    }

    public decimal Open { get; protected set; }
    public decimal High { get; protected set; }
    public decimal Low { get; protected set; }
    public decimal Close { get; protected set; }

    public Ohlc(TimeSpan frameSpan)
    {
        FrameSpan = frameSpan;
        High = decimal.MinValue;
        Low = decimal.MaxValue;
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

    public Ohlc CreateMissingFrame()
    {
        return new Ohlc(this.FrameSpan)
        {
            Start = this.Start + this.FrameSpan,
            Open = this.Close,
            High = this.Close,
            Low = this.Close,
            Close = this.Close,
        };
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
    public decimal Volume { get; protected set; }

    public Ohlcv(TimeSpan frameSpan) : base(frameSpan) { }

    public Ohlcv(TimeSpan frameSpan, DateTime start, decimal open, decimal high, decimal low, decimal close, decimal volume)
        : base(frameSpan)
    {
        Start = start;
        Open = open;
        High = high;
        Low = low;
        Close = close;
        Volume = volume;
    }

    public virtual void Update(DateTime time, decimal price, decimal size)
    {
        base.Update(time, price);
        Volume += Math.Abs(size); // Some of system indicates sell as minus
    }

    public new Ohlcv CreateMissingFrame()
    {
        return new Ohlcv(this.FrameSpan)
        {
            Start = this.Start + this.FrameSpan,
            Open = this.Close,
            High = this.Close,
            Low = this.Close,
            Close = this.Close,
            Volume = decimal.Zero,
        };
    }
}

// OHLC + Start + Volume + VWAP(Volume Weighted Average Price)
public class Ohlcvv : Ohlcv, IOhlcvv
{
    decimal _amount;

    public double VWAP { get; protected set; }

    public Ohlcvv(TimeSpan frameSpan) : base(frameSpan) { }

    public Ohlcvv(TimeSpan frameSpan, DateTime start, decimal open, decimal high, decimal low, decimal close, decimal volume, double vwap)
        : base(frameSpan, start, open, high, low, close, volume)
    {
        VWAP = vwap;
    }

    public Ohlcvv(TimeSpan frameSpan, IEnumerable<IExecution> executions)
        : this(frameSpan)
    {
        executions.ForEach(exec => Update(exec.Time, exec.Price, exec.Size));
    }

    public Ohlcvv(TimeSpan frameSpan, IEnumerable<IOhlcvv> ohlcs)
        : this(frameSpan)
    {
        ohlcs = ohlcs.OrderBy(e => e.Start);

        var first = ohlcs.First();
        Start = first.Start;
        Open = first.Open;
        High = ohlcs.Max(e => e.High);
        Low = ohlcs.Min(e => e.Low);
        Close = ohlcs.Last().Close;
        Volume = ohlcs.Sum(e => e.Volume);
        VWAP = Convert.ToDouble(ohlcs.Sum(e => Convert.ToDecimal(e.VWAP) * e.Volume) / Volume);
    }

    public override void Update(DateTime time, decimal price, decimal size)
    {
        base.Update(time, price, size);
        _amount += price * Math.Abs(size);
        try
        {
            VWAP = Convert.ToDouble(_amount / Volume);
        }
        catch (DivideByZeroException)
        {
            VWAP = 0d;
        }
    }

    public new Ohlcvv CreateMissingFrame()
    {
        return new Ohlcvv(this.FrameSpan)
        {
            Start = this.Start + this.FrameSpan,
            Open = this.Close,
            High = this.Close,
            Low = this.Close,
            Close = this.Close,
            Volume = decimal.Zero,
            VWAP = 0d,
        };
    }
}
