//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Financier.Trading
{
    public class TestAccount : IAccount, IDisposable
    {
        Dictionary<string, IMarket> _markets = new Dictionary<string, IMarket>();

        public TestAccount()
        {
        }

        public void Dispose()
        {
        }

        public virtual void Open()
        {
        }

        internal void RegisterMarket(string marketSymbol, IMarket market)
        {
            _markets.Add(marketSymbol, market);
        }

        public IMarket GetMarket(string marketSymbol)
        {
            return _markets[marketSymbol];
        }

        //=========================================

        // Position management
        protected List<ITrade> Trades { get; } = new List<ITrade>();

        public decimal UnrealizedProfit => Trades.Sum(e => e.UnrealizedProfit);
        public decimal RealizedProfit => Trades.Sum(e => e.RealizedProfit);

        public bool HasOpenPosition(IMarket market)
        {
            throw new NotImplementedException();
        }
    }

    public class AccountCollection : Collection<IAccount>, IAccountCollection
    {
    }
}
