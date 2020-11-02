//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Data;

namespace Financier.Trading
{
    public class Order : IOrder
    {
        #region IOrder implementations
        public virtual DateTime? OpenTime { get; set; }
        public virtual DateTime? CloseTime { get; set; }

        public virtual OrderType OrderType { get; set; }
        public virtual decimal? OrderSize { get; set; }
        public virtual decimal? OrderPrice { get; set; }

        public virtual decimal? TriggerPrice { get; set; }
        public virtual decimal? TrailingOffset { get; set; }
        public virtual decimal? ProfitPrice { get; set; }

        public virtual OrderState State { get; set; }

        public virtual IEnumerable<IExecution> Executions => _execs;
        public virtual decimal? ExecutedPrice { get; set; }
        public virtual decimal? ExecutedSize { get; set; }

        public virtual IReadOnlyList<IOrder> Children => _children;
        #endregion IOrder implementations

        protected List<IExecution> _execs = new List<IExecution>();
        protected List<IOrder> _children;

        #region Constructors
        public Order()
        {
            _children = new List<IOrder>();
        }

        public Order(IOrder child)
        {
            _children = new List<IOrder> { child };
        }

        public Order(IEnumerable<IOrder> children)
        {
            _children = new List<IOrder>(children);
        }
        #endregion Constructors

        public void AddExecution(DateTime time, decimal price, decimal size)
        {
            if (!OrderSize.HasValue)
            {
                throw new ArgumentException();
            }
            if (!ExecutedSize.HasValue)
            {
                ExecutedSize = 0m;
            }

            var exec = new Execution { Time = time, Price = price, Size = size };
            ExecutedSize += size;
            if (ExecutedSize > OrderSize)
            {
                exec.Size -= ExecutedSize.Value - OrderSize.Value;
                ExecutedSize = OrderSize;
            }
            _execs.Add(exec);

            ExecutedPrice = _execs.Sum(e => e.Price * e.Size) / _execs.Sum(e => e.Size); // Calculate average price
            State = (ExecutedSize.Value < OrderSize.Value) ? OrderState.PartiallyExecuted : OrderState.Executed;
        }

        public void ReplaceChildOrder(int childOrderIndex, IOrder order)
        {
            _children[childOrderIndex] = order;
        }
    }
}
