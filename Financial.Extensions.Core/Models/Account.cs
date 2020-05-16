//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Financial.Extensions.Trading
{
    public class Account<TPrice, TSize> : IAccount<TPrice, TSize>
    {
        Dictionary<string, IMarket<TPrice, TSize>> _markets = new Dictionary<string, IMarket<TPrice, TSize>>();

        // Position management
        protected List<ITrade> Trades { get; } = new List<ITrade>();

        public decimal UnrealizedProfit => Trades.Sum(e => e.UnrealizedProfit);
        public decimal RealizedProfit => Trades.Sum(e => e.RealizedProfit);

        public Account()
        {
        }

        public IMarket<TPrice, TSize> GetMarket(string marketSymbol)
        {
            return _markets[marketSymbol];
        }

        public bool HasOpenPosition(IMarket<TPrice, TSize> market)
        {
            throw new NotImplementedException();
        }

        public void RegisterTrade(ITrade pos)
        {
            Trades.Add(pos);
        }
    }

    public class AccountCollection : Collection<IAccount>, IAccountCollection
    {
    }
}
