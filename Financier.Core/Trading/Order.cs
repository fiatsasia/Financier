//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Collections.Generic;

namespace Financier.Trading
{
    public abstract class Order : IOrder
    {
        public virtual DateTime OpenTime { get; protected set; }
        public virtual DateTime CloseTime { get; protected set; } = DateTime.MinValue;
        public virtual OrderState Status { get; protected set; }

        public virtual bool IsClosed => CloseTime != DateTime.MinValue;

        public OrderType OrderType { get; protected set; }
        public virtual decimal OrderPrice => throw new NotSupportedException();
        public virtual decimal OrderSize { get; protected set; }
        public TradeSide Side => Calculator.Sign(OrderSize) > 0 ? TradeSide.Buy : TradeSide.Sell;

        List<IExecution> _execs = new List<IExecution>();
        public virtual IEnumerable<IExecution> Executions => _execs;
        public virtual decimal ExecutedPrice
        {
            get
            {
                if (_execs.Count == 0)
                {
                    return decimal.Zero;
                }
                else if (_execs.Count == 1)
                {
                    return _execs[0].Price;
                }
                else // VWAP
                {
                    var totalSize = 0m;
                    var amount = 0m;
                    foreach (var exec in _execs)
                    {
                        var size = Math.Abs(Calculator.ToDecimal(exec.Size));
                        totalSize += size;
                        amount += Calculator.ToDecimal(exec.Price) * size;
                    }
                    return amount / totalSize;
                }
            }
        }
        public virtual decimal ExecutedSize => _execs.Sum(e => e.Size);

        public virtual IReadOnlyList<IOrder> Children => throw new NotSupportedException();

        public Order()
        {
        }

        public Order(TradeSide side, decimal orderSize)
        {
            OrderType = OrderType.MarketPrice;
            switch (side)
            {
                case TradeSide.Buy:
                    OrderSize = Math.Abs(orderSize);
                    break;

                case TradeSide.Sell:
                    OrderSize = -Math.Abs(orderSize);
                    break;

                default:
                    throw new ArgumentException();
            }
        }

        public virtual void Open(DateTime time)
        {
            OpenTime = time;
        }

        public virtual bool TryExecute(DateTime time, decimal executePrice)
        {
            _execs.Add(new Execution { Time = time, Price = executePrice, Size = OrderSize });
            var execuedSize = _execs.Sum(e => e.Size);
            var compare = Calculator.CompareTo(execuedSize, OrderSize);
            if (execuedSize == OrderSize)
            {
                CloseTime = time;
                Status = OrderState.Filled;
            }
            else if (compare < 0)
            {
                Status = OrderState.PartiallyFilled;
            }
            else
            {
                throw new InvalidOperationException("Executed size is bigger than ordered size.");
            }

            return true;
        }
    }
}
