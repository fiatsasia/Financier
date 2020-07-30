//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public class StopOrder : Order
    {
        public override decimal OrderPrice { get; }

        public StopOrder(decimal stopPrice, decimal size)
        {
            OrderType = OrderType.Stop;
            OrderPrice = stopPrice;
            OrderSize = size;
        }

        public StopOrder(TradeSide side, decimal stopPrice, decimal size)
            : base(side, size)
        {
            OrderType = OrderType.Stop;
            OrderPrice = stopPrice;
        }

        public override bool TryExecute(DateTime time, decimal executePrice)
        {
            if (Side == TradeSide.Buy) // Stop market price buy
            {
                if (Calculator.CompareTo(OrderPrice, executePrice) > 0)
                {
                    return false;
                }
            }
            else //if (Side == TradeSide.Sell) // Stop market price sell
            {
                if (Calculator.CompareTo(OrderPrice, executePrice) < 0)
                {
                    return false;
                }
            }

            return base.TryExecute(time, executePrice);
        }
    }
}
