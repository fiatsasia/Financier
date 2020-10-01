//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

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

        public virtual IEnumerable<IExecution> Executions { get; } = new IExecution[0];
        public virtual decimal? ExecutedPrice { get; }
        public virtual decimal? ExecutedSize { get; }

        public virtual IReadOnlyList<IOrder> Children => _children;
        #endregion IOrder implementations

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

        public void ReplaceChildOrder(int childOrderIndex, IOrder order)
        {
            _children[childOrderIndex] = order;
        }
    }
}
