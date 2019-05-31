using System;
using System.Collections.Generic;
using System.Text;

namespace Financial.Extensions
{
    public class FxTradingAccountModel : IFxTradingAccount
    {
        public void Login(string key, string secret)
        {
        }

        public IFxTradingMarket GetMarket(string marketSymbol)
        {
            return new FxTradingMarketModel(this, marketSymbol);
        }
    }
}
