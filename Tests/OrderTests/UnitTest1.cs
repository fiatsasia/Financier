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
            var order1a = new MarketPriceOrder<double, double>(TradeSide.Buy, 1.0);
            var order1b = new MarketPriceOrder<double, double>(1.0);     // buy
            var order1c = new MarketPriceOrder<double, double>(-1.0);    // sell

            var order2a = new LimitPriceOrder<double, double>(TradeSide.Buy, 300000.0, 1.0);
            var order2b = new LimitPriceOrder<double, double>(300000.0, 1.0);    // buy
            var order2c = new LimitPriceOrder<double, double>(300000.0, -1.0);   // sell
        }

        [TestMethod]
        public void TestPlaceOrder()
        {
            var market = new Market<double, double>();
            var order = new LimitPriceOrder<double, double>(300000.0, 1.0); // buy
            market.PlaceOrder(order);
        }
    }
}
