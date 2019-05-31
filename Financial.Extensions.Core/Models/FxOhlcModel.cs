using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Financial.Extensions
{
    public class FxOhlcModel : IFxOhlcvv
    {
        public DateTime Start { get; private set; } = DateTime.MaxValue;
        public decimal Open { get; private set; }
        public decimal High { get; private set; } = decimal.MinValue;
        public decimal Low { get; private set; } = decimal.MaxValue;
        public decimal Close { get; private set; }
        public double Volume { get; private set; }
        public double VWAP { get; private set; }

        public DateTime End { get; private set; } = DateTime.MinValue;
        public virtual double TypicalPrice { get { return VWAP; } }

        decimal _amount;

        public FxOhlcModel()
        {
        }

        public FxOhlcModel(IFxTradeStream trade)
        {
            Update(trade);
        }

        public FxOhlcModel(IEnumerable<IFxTradeStream> trades)
        {
            trades.ForEach(trade => Update(trade));
        }

        public virtual void Update(IFxTradeStream trade)
        {
            Debug.Assert(trade != null);
            if (trade.Time < Start)
            {
                Start = trade.Time;
                Open = trade.Price;
            }
            if (trade.Time > End)
            {
                End = trade.Time;
                Close = trade.Price;
            }

            High = Math.Max(High, trade.Price);
            Low = Math.Min(Low, trade.Price);
            Volume += unchecked((double)Math.Abs(trade.Size));

            _amount += Convert.ToDecimal(trade.Price * Math.Abs(trade.Size));
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
