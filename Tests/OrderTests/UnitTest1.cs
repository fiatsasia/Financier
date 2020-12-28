//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var factory = new OrderRequestFactory<OrderRequest>();
            var marketBuy = factory.Market(OrderSize);
            var marketSell = factory.Market(-OrderSize);

            var limitBuy = factory.Limit(OrderPrice, OrderSize);
            var limitSell = factory.Limit(OrderPrice, -OrderSize);

            var stopLossBuy = factory.Stop(TriggerPrice, OrderSize);
            var stopLossSell = factory.Stop(TriggerPrice, -OrderSize);

            var stopLossLimitBuy = factory.StopLimit(TriggerPrice, OrderPrice, OrderSize);
            var stopLossLimitSell = factory.StopLimit(TriggerPrice, OrderPrice, -OrderSize);
        }

        [TestMethod]
        public void TestPlaceOrder()
        {
        }
    }
}
