//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
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
        const decimal orderPrice = 1000000m;
        const decimal orderSize = 1.0m;
        const decimal triggerPrice = 1050000m;
        const decimal triggerOffsetPrice = 50000m;
        const decimal triggerOffsetRate = 0.05m;
        const decimal stopPrice = 1050000m;

        [TestMethod]
        public void TestCreateSimpleOrder()
        {
            Order order;

            // Market
            order = OrderFactory.Market(orderSize);
            order = OrderFactory.Market(-orderSize);
            order = OrderFactory.Market(TradeSide.Buy, orderSize);
            order = OrderFactory.Market(TradeSide.Sell, orderSize);

            // Limit
            order = OrderFactory.Limit(orderPrice, orderSize);
            order = OrderFactory.Limit(orderPrice, -orderSize);
            order = OrderFactory.Limit(TradeSide.Buy, orderPrice, orderSize);
            order = OrderFactory.Limit(TradeSide.Sell, orderPrice, orderSize);

            // Stop
            order = OrderFactory.Stop(triggerPrice, orderSize);
            order = OrderFactory.Stop(triggerPrice, -orderSize);
            order = OrderFactory.Stop(TradeSide.Buy, triggerPrice, orderSize);
            order = OrderFactory.Stop(TradeSide.Sell, triggerPrice, orderSize);

            // Stop limit
            order = OrderFactory.StopLimit(triggerPrice, stopPrice, orderSize);
            order = OrderFactory.StopLimit(triggerPrice, stopPrice, -orderSize);
            order = OrderFactory.StopLimit(TradeSide.Buy, triggerPrice, stopPrice, orderSize);
            order = OrderFactory.StopLimit(TradeSide.Sell, triggerPrice, stopPrice, orderSize);

            // Trailing stop

            // Trailing stop limit
        }
    }
}
