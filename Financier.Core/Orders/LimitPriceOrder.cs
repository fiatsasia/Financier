//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public class LimitPriceOrder<TPrice, TSize> : Order<TPrice, TSize>
    {
        public override TPrice OrderPrice { get; }

        public LimitPriceOrder(TPrice price, TSize size)
        {
            OrderType = OrderType.LimitPrice;
            OrderPrice = price;
            OrderSize = size;
        }

        public LimitPriceOrder(TradeSide side, TPrice price, TSize size)
            : base(side, size)
        {
            OrderType = OrderType.LimitPrice;
            OrderPrice = price;
        }

        public override bool TryExecute(DateTime time, TPrice executePrice)
        {
            if (Side == TradeSide.Buy)
            {
                if (Calculator.CompareTo(OrderPrice, executePrice) < 0)
                {
                    return false;
                }
            }
            else //if (Side == TradeSide.Sell)
            {
                if (Calculator.CompareTo(OrderPrice, executePrice) > 0)
                {
                    return false;
                }
            }

            return base.TryExecute(time, executePrice);
        }
    }
}
