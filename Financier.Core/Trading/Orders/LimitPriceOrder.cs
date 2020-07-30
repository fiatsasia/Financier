//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public class LimitPriceOrder : Order
    {
        public override decimal OrderPrice { get; }

        public LimitPriceOrder(decimal price, decimal size)
        {
            OrderType = OrderType.LimitPrice;
            OrderPrice = price;
            OrderSize = size;
        }

        public LimitPriceOrder(TradeSide side, decimal price, decimal size)
            : base(side, size)
        {
            OrderType = OrderType.LimitPrice;
            OrderPrice = price;
        }

        public override bool TryExecute(DateTime time, decimal executePrice)
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
