//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
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
    public class OrderPosition2
    {
        public Ulid Id { get; }
        public Ulid OpenExecutionId { get; set; }
        public int OpenExecutionIndex { get; set; }
        public Ulid? CloseExecutionId { get; set; }
        public int? CloseExecutionIndex { get; set; }
        public virtual DateTime OpenTime { get; }
        public virtual DateTime? CloseTime { get; }
        public TradeSide Side { get; }
        public virtual decimal OpenPrice { get; }
        public virtual decimal? ClosePrice { get; }
        public virtual decimal Size { get; }
        public virtual decimal? Commission { get; }
        public virtual IReadOnlyDictionary<string, object> Metadata { get; protected set; }

        public OrderPositions Positions { get; }

        public OrderPosition2()
        {
            Id = Ulid.NewUlid();
        }

        internal OrderPosition2(OrderPositions positions, ActivePosition pos)
        {
            Positions = positions;

            Id = pos.Id;
            OpenExecutionId = pos.OpenExecutionId;
            OpenExecutionIndex = pos.OpenExecutionIndex;
            OpenTime = pos.OpenTime;
            Side = pos.OpenSize > 0m ? TradeSide.Buy : TradeSide.Sell;
            OpenPrice = pos.OpenPrice;
            Size = Math.Abs(pos.CurrentSize);
        }

        internal OrderPosition2(OrderPositions positions, ActivePosition pos, OrderExecution exec)
        {
            Positions = positions;

            Id = pos.Id;
            OpenExecutionId = pos.OpenExecutionId;
            OpenExecutionIndex = pos.OpenExecutionIndex;
            //CloseExecutionId = exec.Id;
            //CloseExecutionIndex = exec.Index;
            OpenTime = pos.OpenTime;
            CloseTime = exec.Time;
            Side = pos.OpenSize > 0m ? TradeSide.Buy : TradeSide.Sell;
            OpenPrice = pos.OpenPrice;
            ClosePrice = exec.Price;
            Size = Math.Abs(pos.CurrentSize);
        }

        public virtual decimal? Profit => ClosePrice.HasValue ? Math.Floor((ClosePrice.Value - OpenPrice) * (Side == TradeSide.Buy ? Size : -Size)) : default;
        public virtual bool IsOpened => !CloseTime.HasValue;
        public bool IsClosed => CloseTime.HasValue;

        public decimal? NetProfit => Profit;
    }

    class ActivePosition
    {
        public Ulid Id { get; private set; }
        public Ulid OpenExecutionId { get; private set; }
        public int OpenExecutionIndex { get; private set; }
        public DateTime OpenTime { get; private set; }
        public decimal OpenPrice { get; private set; }
        public decimal OpenSize { get; private set; }

        public decimal CurrentSize { get; private set; }

        private ActivePosition() { }

        public ActivePosition(OrderExecution exec)
        {
            Id = Ulid.NewUlid();
            //OpenExecutionId = exec.Id;
            //OpenExecutionIndex = exec.Index;
            OpenTime = exec.Time;
            OpenPrice = exec.Price;
            CurrentSize = OpenSize = exec.Size;
        }

        internal ActivePosition SeparateClosedPosition(decimal closingSize)
        {
            var closedPos = new ActivePosition
            {
                Id = this.Id,
                OpenExecutionId = this.OpenExecutionId,
                OpenExecutionIndex = this.OpenExecutionIndex,
                OpenTime = this.OpenTime,
                OpenPrice = this.OpenPrice,
                OpenSize = this.OpenSize,
                CurrentSize = -closingSize,
            };
            this.Id = Ulid.NewUlid(); // Refresh to new position
            CurrentSize += closingSize;
            return closedPos;
        }
    }

    public class OrderPositions
    {
        Queue<ActivePosition> _q = new();

        public decimal TotalOpenSize => Math.Abs(_q.Sum(e => e.CurrentSize));

        public IEnumerable<OrderPosition2> GetOpenPositions()
        {
            return _q.ToList().Select(e => new OrderPosition2(this, e));
        }

        public IEnumerable<OrderPosition2> Update(OrderExecution exec)
        {
            if (_q.Count == 0 || Math.Sign(_q.Peek().OpenSize) == Math.Sign(exec.Size))
            {
                var pos = new ActivePosition(exec);
                _q.Enqueue(pos);
                return new OrderPosition2[] { new OrderPosition2(this, pos) };
            }

            // Process to another side
            var closingSize = exec.Size;
            var closedPos = new List<ActivePosition>();
            while (Math.Abs(closingSize) > 0m && _q.Count > 0)
            {
                var pos = _q.Peek();
                if (Math.Abs(closingSize) >= Math.Abs(pos.CurrentSize))
                {
                    closingSize += pos.CurrentSize;
                    closedPos.Add(_q.Dequeue());
                    continue;
                }
                if (Math.Abs(closingSize) < Math.Abs(pos.CurrentSize))
                {
                    closedPos.Add(pos.SeparateClosedPosition(closingSize));
                    closingSize = 0;
                    break;
                }
            }
            var result = closedPos.Select(e => new OrderPosition2(this, e, exec)).ToList();

            if (closingSize > 0m)
            {
                var pos = new ActivePosition(exec);
                _q.Enqueue(pos);
                result.Add(new OrderPosition2(this, pos));
            }

            return result;
        }
    }
}
