//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public class MarketPriceOrder : Order
    {
        public MarketPriceOrder(decimal size)
        {
            OrderType = OrderType.MarketPrice;
            OrderSize = size;
        }

        public MarketPriceOrder(TradeSide side, decimal size)
            : base(side, size)
        {
            OrderType = OrderType.MarketPrice;
        }
    }
}
