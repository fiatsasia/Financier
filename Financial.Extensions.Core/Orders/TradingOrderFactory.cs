//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public class TradingOrderFactory<TPrice, TSize> : ITradingOrderFactory<TPrice, TSize>
    {
        //
        // Simple orders
        //
        public virtual ITradingOrder<TPrice, TSize> CreateMarketPriceOrder(TSize size)
        {
            return new TradingMarketPriceOrder<TPrice, TSize>(size);
        }

        public virtual ITradingOrder<TPrice, TSize> CreateLimitPriceOrder(TPrice price, TSize size)
        {
            return new TradingLimitPriceOrder<TPrice, TSize>(price, size);
        }

        //
        // Settlement orders
        //
        public virtual ITradingOrder<TPrice, TSize> CreateSettlementMarketPriceOrder(ITradingPosition<TPrice, TSize> position)
        {
            return new TradingMarketPriceOrder<TPrice, TSize>(Calculator.Invert(position.Size), position);
        }

        public virtual ITradingOrder<TPrice, TSize> CreateSettlementLimitPriceOrder(ITradingPosition<TPrice, TSize> position, TPrice price)
        {
            return new TradingLimitPriceOrder<TPrice, TSize>(price, Calculator.Invert(position.Size), position);
        }

        public virtual ITradingOrder<TPrice, TSize> CreateStopAndReverseOrder(ITradingPosition<TPrice, TSize> position)
        {
            return new StopAndReverseOrder<TPrice, TSize>(position);
        }

        //
        // Conditional orders
        //
        public virtual ITradingOrder<TPrice, TSize> CreateStopOrder(TSize size, TPrice stopTriggerPrice)
        {
            throw new NotImplementedException();
        }

        public virtual ITradingOrder<TPrice, TSize> CreateStopLimitOrder(TSize size, TPrice price, TPrice stopTriggerPrice)
        {
            throw new NotImplementedException();
        }

        public virtual ITradingOrder<TPrice, TSize> CreateTrailingStopOrder(TSize size, TPrice trailingStopPriceOffset)
        {
            throw new NotImplementedException();
        }

        public virtual ITradingOrder<TPrice, TSize> CreateIFD(ITradingOrder first, ITradingOrder second)
        {
            throw new NotImplementedException();
        }

        public virtual ITradingOrder<TPrice, TSize> CreateOCO(ITradingOrder first, ITradingOrder second)
        {
            throw new NotImplementedException();
        }

        public virtual ITradingOrder<TPrice, TSize> CreateIFDOCO(ITradingOrder ifdone, ITradingOrder ocoFirst, ITradingOrder ocoSecond)
        {
            throw new NotImplementedException();
        }
    }
}
