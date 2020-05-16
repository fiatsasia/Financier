//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financial.Extensions.Trading
{
    class OrderFactory<TPrice, TSize> : OrderFactoryBase<TPrice, TSize>
    {
        public override IOrder<TPrice, TSize> CreateMarketPriceOrder(TSize size)
        {
            return new MarketPriceOrder<TPrice, TSize>(size);
        }

        public override IOrder<TPrice, TSize> CreateLimitPriceOrder(TPrice price, TSize size)
        {
            return new LimitPriceOrder<TPrice, TSize>(price, size);
        }

        public override IOrder<TPrice, TSize> CreateStopOrder(TPrice stopPrice, TSize size)
        {
            return new StopOrder<TPrice, TSize>(stopPrice, size);
        }

        public override IOrder<TPrice, TSize> CreateStopAndReverseOrder(TPrice stopPrice, TSize size)
        {
            return new StopAndReverseOrder<TPrice, TSize>(stopPrice, size);
        }
    }
}
