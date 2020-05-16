//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions.Trading
{
    public class MarketPriceOrder<TPrice, TSize> : Order<TPrice, TSize>
    {
        public MarketPriceOrder(TSize size)
        {
            OrderType = OrderType.MarketPrice;
            OrderSize = size;
        }

        public MarketPriceOrder(TradeSide side, TSize size)
            : base(side, size)
        {
            OrderType = OrderType.MarketPrice;
        }
    }
}
