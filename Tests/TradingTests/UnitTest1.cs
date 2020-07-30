using Microsoft.VisualStudio.TestTools.UnitTesting;
using Financier.Trading;

namespace TradingTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AccountTest()
        {
            using (var account = new Account())
            {
            }
        }

        [TestMethod]
        public void MarketTest()
        {
            using (var market = new Market())
            {
            }
        }

        [TestMethod]
        public void OrderTest()
        {
            using (var account = new Account())
            using (var market = new Market(account, "SYMBOL"))
            {
                var order = new MarketPriceOrder(-1.0m);
                market.PlaceOrder(order);
            }
        }

        [TestMethod]
        public async void OrderAynscTest()
        {
            using (var account = new Account())
            using (var market = new Market(account, "SYMBOL"))
            {
                var order = new MarketPriceOrder(-1.0m);
                await market.PlaceOrderAsync(order);
            }
        }
    }
}
