//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financial.Extensions.Trading
{
    public interface IAccount
    {
        decimal UnrealizedProfit { get; }
        decimal RealizedProfit { get; }

        bool HasOpenPosition<TPrice, TSize>(IMarket<TPrice, TSize> market);
        ITrade<TPrice, TSize> GetLastOpenPosition<TPrice, TSize>(IMarket<TPrice, TSize> market);

        void Login(string key, string secret);
        void RegisterPosition(ITrade pos);

        void RegisterMarket<TPrice, TSize>(string marketSymbol, IMarket<TPrice, TSize> market);
    }
}
