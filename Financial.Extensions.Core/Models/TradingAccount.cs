//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Collections.Generic;

namespace Financial.Extensions
{
    public class TradingAccount : ITradingAccount
    {
        Dictionary<string, ITradingMarket> _markets = new Dictionary<string, ITradingMarket>();

        // Position management
        protected List<ITradingPosition> Positions { get; } = new List<ITradingPosition>();

        public decimal UnrealizedProfit => Positions.Sum(e => e.UnrealizedProfit);
        public decimal RealizedProfit => Positions.Sum(e => e.RealizedProfit);

        public TradingAccount()
        {
        }

        public void RegisterMarket<TPrice, TSize>(string marketSymbol, ITradingMarket<TPrice, TSize> market)
        {
            _markets[marketSymbol] = market;
        }

        public bool HasOpenPosition<TPrice, TSize>(ITradingMarket<TPrice, TSize> market)
        {
            throw new NotImplementedException();
        }

        public ITradingPosition<TPrice, TSize> GetLastOpenPosition<TPrice, TSize>(ITradingMarket<TPrice, TSize> market)
        {
            throw new NotImplementedException();
        }

        public ITradingPosition GetLastOpenPosition()
        {
            return null;
        }

        public void RegisterPosition(ITradingPosition pos)
        {
            Positions.Add(pos);
        }

        // Not support functionalities
        public void Login(string key, string secret) => throw new NotSupportedException();
    }
}
