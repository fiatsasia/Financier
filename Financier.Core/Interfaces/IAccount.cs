//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System.Collections.Generic;

namespace Financier.Trading
{
    public interface IAccount
    {
    }

    public interface IAccount<TPrice, TSize> : IAccount
    {
        decimal UnrealizedProfit { get; }
        decimal RealizedProfit { get; }

        bool HasOpenPosition(IMarket<TPrice, TSize> market);

        void RegisterTrade(ITrade trade);

        IMarket<TPrice, TSize> GetMarket(string marketSymbol);
    }

    public interface IAccountCollection : ICollection<IAccount>
    {
    }
}
