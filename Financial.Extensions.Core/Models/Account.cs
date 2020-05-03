//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Collections.Generic;

namespace Financial.Extensions.Trading
{
    public class Account : IAccount
    {
        Dictionary<string, IMarket> _markets = new Dictionary<string, IMarket>();

        // Position management
        protected List<ITrade> Positions { get; } = new List<ITrade>();

        public decimal UnrealizedProfit => Positions.Sum(e => e.UnrealizedProfit);
        public decimal RealizedProfit => Positions.Sum(e => e.RealizedProfit);

        public Account()
        {
        }

        public void RegisterMarket<TPrice, TSize>(string marketSymbol, IMarket<TPrice, TSize> market)
        {
            _markets[marketSymbol] = market;
        }

        public bool HasOpenPosition<TPrice, TSize>(IMarket<TPrice, TSize> market)
        {
            throw new NotImplementedException();
        }

        public ITrade<TPrice, TSize> GetLastOpenPosition<TPrice, TSize>(IMarket<TPrice, TSize> market)
        {
            throw new NotImplementedException();
        }

        public ITrade GetLastOpenPosition()
        {
            return null;
        }

        public void RegisterPosition(ITrade pos)
        {
            Positions.Add(pos);
        }

        // Not support functionalities
        public void Login(string key, string secret) => throw new NotSupportedException();
    }
}
