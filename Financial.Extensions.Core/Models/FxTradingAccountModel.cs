//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financial.Extensions
{
    public class FxTradingAccountModel : IFxTradingAccount
    {
        public string ProviderName { get; set; }

        public void Login(string key, string secret)
        {
        }

        public IFxTradingMarket GetMarket(string marketSymbol)
        {
            return new FxTradingMarketModel(this, marketSymbol);
        }
    }
}
