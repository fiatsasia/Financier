//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financier;
using Financier.Trading;

namespace OrderTests
{
    [TestClass]
    public class UnitTest1
    {
        const decimal OrderPrice = 1000000m;
        const decimal OrderSize = 1.0m;
        const decimal TriggerPrice = 1050000m;

        [TestMethod]
        public void TestCreateOrder()
        {
            var factory = new OrderFactory();
            var marketBuy = factory.MarketPrice(OrderSize);
            var marketSell = factory.MarketPrice(-OrderSize);

            var limitBuy = factory.LimitPrice(OrderPrice, OrderSize);
            var limitSell = factory.LimitPrice(OrderPrice, -OrderSize);

            var stopLossBuy = factory.StopLoss(TriggerPrice, OrderSize);
            var stopLossSell = factory.StopLoss(TriggerPrice, -OrderSize);

            var stopLossLimitBuy = factory.StopLimit(TriggerPrice, OrderPrice, OrderSize);
            var stopLossLimitSell = factory.StopLimit(TriggerPrice, OrderPrice, -OrderSize);
        }

        [TestMethod]
        public void TestPlaceOrder()
        {
        }
    }
}
