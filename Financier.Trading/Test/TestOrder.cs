//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Financier.Trading
{
    public class TestOrder : Order
    {
        List<IExecution> _execs = new List<IExecution>();
        public override IEnumerable<IExecution> Executions => _execs;
        public override decimal? ExecutedPrice { get; }
        public override decimal? ExecutedSize => _execs.Sum(e => e.Size);

        public TestOrder(decimal orderSize)
        {
            OrderType = OrderType.MarketPrice;
            OrderSize = orderSize;
        }

        public void Open(DateTime time)
        {
            OpenTime = time;
        }

        public bool TryExecute(DateTime time, decimal executePrice)
        {
            _execs.Add(new Execution { Time = time, Price = executePrice, Size = OrderSize.Value });
            var execuedSize = _execs.Sum(e => e.Size);
            var compare = Calculator.CompareTo(execuedSize, OrderSize);
            if (execuedSize == OrderSize)
            {
                CloseTime = time;
                State = OrderState.Executed;
            }
            else if (compare < 0)
            {
                State = OrderState.Executing;
            }
            else
            {
                throw new InvalidOperationException("Executed size is bigger than ordered size.");
            }

            return true;
        }
    }
}
