//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions.Trading
{
    public class MarketPriceOrder<TPrice, TSize> : Order<TPrice, TSize>
    {
        public MarketPriceOrder(TSize orderSize)
        {
            OrderType = OrderType.MarketPrice;
            OrderSize = orderSize;
        }

        public MarketPriceOrder(TradeSide side, TSize orderSize)
            : base(side, orderSize)
        {
            OrderType = OrderType.MarketPrice;
        }
    }
}
