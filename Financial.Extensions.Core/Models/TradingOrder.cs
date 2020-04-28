//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financial.Extensions
{
    public class TradingOrder<TPrice, TSize> : ITradingOrder<TPrice, TSize>
    {
        public DateTime OpenTime { get; protected set; }
        public DateTime CloseTime { get; protected set; }
        public TradingOrderState Status { get; protected set; }

        public TradingOrderType OrderType { get; protected set; }
        public TPrice OrderPrice { get; protected set; }
        public TSize OrderSize { get; protected set; }
        public TradeSide Side => Calculator.Sign(OrderSize) > 0 ? TradeSide.Buy : TradeSide.Sell;

        public TPrice ExecutedPrice { get; protected set; }
        public TSize ExecutedSize { get; protected set; }

        public ITradingPosition<TPrice, TSize> Position { get; protected set; }
        public bool IsSettlmentOrder => Position != null;
        public virtual bool HasChildOrder => false;

        public ITradingOrder<TPrice, TSize> Parent { get; protected set; }

        static List<ITradingOrder<TPrice, TSize>> _emptyChildren = new List<ITradingOrder<TPrice, TSize>>();
        public virtual IReadOnlyList<ITradingOrder<TPrice, TSize>> Children => _emptyChildren;

        public TradingOrder()
        {
        }

        public TradingOrder(TradeSide side, TSize orderSize)
        {
            OrderType = TradingOrderType.MarketPrice;
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

        public virtual bool CanExecute(TPrice price)
        {
            return false;
        }

        public virtual void Execute(DateTime time, TPrice executePrice)
        {
            CloseTime = time;
            ExecutedSize = OrderSize;
            Status = TradingOrderState.Filled;
        }

        public virtual void ExecutePartial(DateTime time, TPrice executePrice, TSize executeSize)
        {
            ExecutedSize = Calculator.Add(ExecutedSize, executeSize);
            var compare = Calculator.CompareTo(ExecutedSize, OrderSize);
            if (compare == 0)
            {
                CloseTime = time;
                Status = TradingOrderState.Filled;
            }
            else if (compare < 0)
            {
                Status = TradingOrderState.PartiallyFilled;
            }
            else
            {
                throw new InvalidOperationException("Executed size is bigger than ordered size.");
            }
        }
    }
}
