using System;
using System.Collections.Generic;
using System.Text;

namespace Financier.Trading
{
    public class OrderPosition
    {
        public virtual DateTime OpenTime { get; }
        public virtual decimal OpenPrice { get; }
        public virtual DateTime? CloseTime { get; }
        public virtual decimal? ClosePrice { get; }
        public virtual decimal Size { get; }
        public virtual decimal? Profit { get; }
        public virtual decimal? Commission { get; }

        public OrderExecution OpenExecution { get; }
        public IReadOnlyCollection<OrderExecution> CloseExecutions { get; }
    }
}
