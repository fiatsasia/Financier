//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public class TradingMarketPriceOrder<TPrice, TSize> : TradingOrder<TPrice, TSize>
    {
        public TradingMarketPriceOrder(TSize orderSize)
        {
            OrderType = TradingOrderType.MarketPrice;
            OrderSize = orderSize;
        }

        public TradingMarketPriceOrder(TradeSide side, TSize orderSize)
            : base(side, orderSize)
        {
            OrderType = TradingOrderType.MarketPrice;
        }

        public TradingMarketPriceOrder(TSize orderSize, ITradingPosition<TPrice, TSize> position)
        {
            OrderType = TradingOrderType.MarketPrice;
            OrderSize = orderSize;
            Position = position;
        }

        public TradingMarketPriceOrder(TSize orderSize, ITradingOrder<TPrice, TSize> parent)
            : this(orderSize)
        {
            Parent = parent;
        }

        public TradingMarketPriceOrder(TSize orderSize, ITradingPosition<TPrice, TSize> position, ITradingOrder<TPrice, TSize> parent)
            : this(orderSize, position)
        {
            Parent = parent;
        }

        public override bool CanExecute(TPrice price)
        {
            return true;
        }
    }
}
