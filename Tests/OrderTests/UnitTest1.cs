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
        [TestMethod]
        public void TestCreateOrder()
        {
            var order1a = new MarketPriceOrder(TradeSide.Buy, 1.0m);
            var order1b = new MarketPriceOrder(1.0m);     // buy
            var order1c = new MarketPriceOrder(-1.0m);    // sell

            var order2a = new LimitPriceOrder(TradeSide.Buy, 300000.0m, 1.0m);
            var order2b = new LimitPriceOrder(300000.0m, 1.0m);    // buy
            var order2c = new LimitPriceOrder(300000.0m, -1.0m);   // sell
        }

        [TestMethod]
        public void TestPlaceOrder()
        {
            var market = new Market();
            var order = new LimitPriceOrder(300000.0m, 1.0m); // buy
            market.PlaceOrder(order);
        }
    }
}
