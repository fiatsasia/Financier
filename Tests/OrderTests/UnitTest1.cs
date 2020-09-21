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
            var marketBuy = Order.MarketPrice(OrderSize);
            var marketSell = Order.MarketPrice(-OrderSize);

            var limitBuy = Order.LimitPrice(OrderPrice, OrderSize);
            var limitSell = Order.LimitPrice(OrderPrice, -OrderSize);

            var stopLossBuy = Order.StopLoss(TriggerPrice, OrderSize);
            var stopLossSell = Order.StopLoss(TriggerPrice, -OrderSize);

            var stopLossLimitBuy = Order.StopLossLimit(TriggerPrice, OrderPrice, OrderSize);
            var stopLossLimitSell = Order.StopLossLimit(TriggerPrice, OrderPrice, -OrderSize);
        }

        [TestMethod]
        public void TestPlaceOrder()
        {
        }
    }
}
