//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions.Trading
{
    public class StopOrder<TPrice, TSize> : Order<TPrice, TSize>
    {
        public override TPrice OrderPrice { get; }

        public StopOrder(TPrice stopPrice, TSize orderSize)
        {
            OrderType = OrderType.Stop;
            OrderPrice = stopPrice;
            OrderSize = orderSize;
        }

        public StopOrder(TradeSide side, TPrice stopPrice, TSize orderSize)
            : base(side, orderSize)
        {
            OrderType = OrderType.Stop;
            OrderPrice = stopPrice;
        }

        public override bool TryExecute(DateTime time, TPrice executePrice)
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
