//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financier.Trading
{
    class OrderFactory : OrderFactoryBase
    {
        public override IOrder MarketPrice(decimal size)
        {
            return new MarketPriceOrder(size);
        }

        public override IOrder LimitPrice(decimal price, decimal size)
        {
            return new LimitPriceOrder(price, size);
        }

        public override IOrder StopLoss(decimal stopPrice, decimal size)
        {
            return new StopOrder(stopPrice, size);
        }

        public override IOrder StopAndReverse(decimal stopPrice, decimal size)
        {
            return new StopAndReverseOrder(stopPrice, size);
        }
    }
}
