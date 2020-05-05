//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions.Trading
{
    public class LimitPriceOrder<TPrice, TSize> : Order<TPrice, TSize>
    {
        public override TPrice OrderPrice { get; }

        public LimitPriceOrder(TPrice orderPrice, TSize orderSize)
        {
            OrderType = OrderType.LimitPrice;
            OrderPrice = orderPrice;
            OrderSize = orderSize;
        }

        public LimitPriceOrder(TradeSide side, TPrice orderPrice, TSize orderSize)
            : base(side, orderSize)
        {
            OrderType = OrderType.LimitPrice;
            OrderPrice = orderPrice;
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
