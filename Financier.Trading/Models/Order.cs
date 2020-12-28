//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Linq;
using System.Collections.Generic;

namespace Financier.Trading
{
    public class Order : IOrder
    {
        #region Order request parameters
        public OrderType OrderType => Request.OrderType;
        public decimal? OrderSize => Request.OrderSize;
        public decimal? OrderPrice => Request.OrderPrice;
        public decimal? TriggerPrice => Request.TriggerPrice;
        public decimal? StopPrice => Request.StopPrice;
        public decimal? TrailingOffset => Request.TrailingOffset;
        public decimal? ProfitPrice => Request.ProfitPrice;
        #endregion Order request parameters

        #region IOrder implementations
        public virtual DateTime? OpenTime { get; set; }
        public virtual DateTime? CloseTime { get; set; }
        public virtual DateTime? ExpirationDate => throw new NotSupportedException();
        public virtual OrderState State { get; set; }

        public virtual IEnumerable<IExecution> Executions => _execs;
        public virtual decimal? ExecutedPrice { get; set; }
        public virtual decimal? ExecutedSize { get; set; }

        public virtual IReadOnlyList<IOrder> Children => _children;
        #endregion IOrder implementations

        protected List<IExecution> _execs = new List<IExecution>();
        protected List<IOrder> _children;

        public IOrderRequest Request { get; }

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

        public Order(IOrderRequest request)
        {
            Request = request;
            _children = ((request.Children?.Length ?? 0) > 0) ? request.Children.Select(e => new Order(e)).Cast<IOrder>().ToList() : new();
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
