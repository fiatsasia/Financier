//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Collections.Generic;

namespace Financial.Extensions.Trading
{
    public abstract class Order<TPrice, TSize> : IOrder<TPrice, TSize>
    {
        public virtual DateTime OpenTime { get; protected set; }
        public virtual DateTime CloseTime { get; protected set; } = DateTime.MinValue;
        public virtual OrderState Status { get; protected set; }

        public virtual bool IsClosed => CloseTime != DateTime.MinValue;

        public OrderType OrderType { get; protected set; }
        public virtual TPrice OrderPrice => throw new NotSupportedException();
        public virtual TSize OrderSize { get; protected set; }
        public TradeSide Side => Calculator.Sign(OrderSize) > 0 ? TradeSide.Buy : TradeSide.Sell;

        List<IExecution<TPrice, TSize>> _execs = new List<IExecution<TPrice, TSize>>();
        public virtual IEnumerable<IExecution<TPrice, TSize>> Executions => _execs;
        public virtual TPrice ExecutedPrice
        {
            get
            {
                if (_execs.Count == 0)
                {
                    return Calculator.Zero<TPrice>();
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
                    return Calculator.Cast<TPrice>(amount / totalSize);
                }
            }
        }
        public virtual TSize ExecutedSize => _execs.Sum(e => e.Size);

        public virtual IReadOnlyList<IOrder<TPrice, TSize>> Children => throw new NotSupportedException();

        public Order()
        {
        }

        public Order(TradeSide side, TSize orderSize)
        {
            OrderType = OrderType.MarketPrice;
            switch (side)
            {
                case TradeSide.Buy:
                    OrderSize = Calculator.Abs(orderSize);
                    break;

                case TradeSide.Sell:
                    OrderSize = Calculator.Invert(Calculator.Abs(orderSize));
                    break;

                default:
                    throw new ArgumentException();
            }
        }

        public virtual void Open(DateTime time)
        {
            OpenTime = time;
        }

        public virtual bool TryExecute(DateTime time, TPrice executePrice)
        {
            _execs.Add(new Execution<TPrice, TSize> { Time = time, Price = executePrice, Size = OrderSize });
            var execuedSize = _execs.Sum(e => e.Size);
            var compare = Calculator.CompareTo(execuedSize, OrderSize);
            if (compare == 0)
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
