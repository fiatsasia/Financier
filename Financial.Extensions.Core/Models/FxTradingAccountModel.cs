//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/feedex/
//

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
