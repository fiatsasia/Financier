//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financier.Trading
{
    class OrderFactory : OrderFactoryBase
    {
        public override IOrder CreateMarketPriceOrder(decimal size)
        {
            return new MarketPriceOrder(size);
        }

        public override IOrder CreateLimitPriceOrder(decimal price, decimal size)
        {
            return new LimitPriceOrder(price, size);
        }

        public override IOrder CreateStopOrder(decimal stopPrice, decimal size)
        {
            return new StopOrder(stopPrice, size);
        }

        public override IOrder CreateStopAndReverseOrder(decimal stopPrice, decimal size)
        {
            return new StopAndReverseOrder(stopPrice, size);
        }
    }
}
