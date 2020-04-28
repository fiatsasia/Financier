//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financial.Extensions;

namespace OrderTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreateOrder()
        {
            var order1a = new TradingMarketPriceOrder<double, double>(TradeSide.Buy, 1.0);
            var order1b = new TradingMarketPriceOrder<double, double>(1.0);     // buy
            var order1c = new TradingMarketPriceOrder<double, double>(-1.0);    // sell

            var order2a = new TradingLimitPriceOrder<double, double>(TradeSide.Buy, 300000.0, 1.0);
            var order2b = new TradingLimitPriceOrder<double, double>(300000.0, 1.0);    // buy
            var order2c = new TradingLimitPriceOrder<double, double>(300000.0, -1.0);   // sell
        }

        [TestMethod]
        public void TestPlaceOrder()
        {
            var market = new TradingMarket<double, double>();
            var order = new TradingLimitPriceOrder<double, double>(300000.0, 1.0); // buy
            market.PlaceOrder(order);
        }
    }
}
