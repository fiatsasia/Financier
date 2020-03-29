//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Financial.Extensions
{
    public interface ITradingAccount
    {
        string ProviderName { get; }

        void Login(string key, string secret);
    }

    public interface ITradingAccount<TPrice, TSize> : ITradingAccount
    {
        ITradingMarket<TPrice, TSize> GetMarket(string marketSymbol);
    }
}
