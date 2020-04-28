//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financial.Extensions
{
    public interface ITradingAccount
    {
        decimal UnrealizedProfit { get; }
        decimal RealizedProfit { get; }

        bool HasOpenPosition<TPrice, TSize>(ITradingMarket<TPrice, TSize> market);
        ITradingPosition<TPrice, TSize> GetLastOpenPosition<TPrice, TSize>(ITradingMarket<TPrice, TSize> market);

        void Login(string key, string secret);
        void RegisterPosition(ITradingPosition pos);

        void RegisterMarket<TPrice, TSize>(string marketSymbol, ITradingMarket<TPrice, TSize> market);
    }
}
