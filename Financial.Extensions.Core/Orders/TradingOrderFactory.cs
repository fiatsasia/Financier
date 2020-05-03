//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions.Trading
{
    public class OrderFactory<TPrice, TSize> : IOrderFactory<TPrice, TSize>
    {
        //
        // Simple orders
        //
        public virtual IOrder<TPrice, TSize> CreateMarketPriceOrder(TSize size)
        {
            return new MarketPriceOrder<TPrice, TSize>(size);
        }

        public virtual IOrder<TPrice, TSize> CreateLimitPriceOrder(TPrice price, TSize size)
        {
            return new LimitPriceOrder<TPrice, TSize>(price, size);
        }

        //
        // Conditional orders
        //
        public virtual IOrder<TPrice, TSize> CreateStopOrder(TSize size, TPrice stopTriggerPrice)
        {
            throw new NotImplementedException();
        }

        public virtual IOrder<TPrice, TSize> CreateStopLimitOrder(TSize size, TPrice price, TPrice stopTriggerPrice)
        {
            throw new NotImplementedException();
        }

        public virtual IOrder<TPrice, TSize> CreateTrailingStopOrder(TSize size, TPrice trailingStopPriceOffset)
        {
            throw new NotImplementedException();
        }

        public virtual IOrder<TPrice, TSize> CreateIFD(IOrder first, IOrder second)
        {
            throw new NotImplementedException();
        }

        public virtual IOrder<TPrice, TSize> CreateOCO(IOrder first, IOrder second)
        {
            throw new NotImplementedException();
        }

        public virtual IOrder<TPrice, TSize> CreateIFDOCO(IOrder ifdone, IOrder ocoFirst, IOrder ocoSecond)
        {
            throw new NotImplementedException();
        }
    }
}
