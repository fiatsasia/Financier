//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public class TradingLimitPriceOrder<TPrice, TSize> : TradingOrder<TPrice, TSize>
    {
        public TradingLimitPriceOrder(TPrice orderPrice, TSize orderSize)
        {
            OrderType = TradingOrderType.LimitPrice;
            OrderPrice = orderPrice;
            OrderSize = orderSize;
        }

        public TradingLimitPriceOrder(TradeSide side, TPrice orderPrice, TSize orderSize)
            : base(side, orderSize)
        {
            OrderType = TradingOrderType.LimitPrice;
            OrderPrice = orderPrice;
        }

        public TradingLimitPriceOrder(TPrice orderPrice, TSize orderSize, ITradingPosition<TPrice, TSize> position)
        {
            OrderType = TradingOrderType.LimitPrice;
            OrderPrice = orderPrice;
            OrderSize = orderSize;
            Position = position;
        }

        public override bool CanExecute(TPrice price)
        {
            if (Side == TradeSide.Buy)
            {
                return Calculator.CompareTo(OrderPrice, price) >= 0;
            }
            else //if (Side == TradeSide.Sell)
            {
                return Calculator.CompareTo(OrderPrice, price) <= 0;
            }
        }
    }
}
