//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financial.Extensions
{
    public class TradingAccountModel<TPrice, TSize> : ITradingAccount<TPrice, TSize>
    {
        public string ProviderName { get; set; }

        public void Login(string key, string secret)
        {
        }

        public ITradingMarket<TPrice, TSize> GetMarket(string marketSymbol)
        {
            return new TradingMarketModel<TPrice, TSize>(this, marketSymbol);
        }
    }
}
