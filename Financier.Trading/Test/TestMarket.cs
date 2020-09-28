//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financier.Trading
{
    public class TestMarket : IMarket, IDisposable
    {
        public string MarketSymbol { get; private set; }
        public decimal LastTradedPrice { get; private set; }
        public DateTime LastUpdatedTime { get; private set; }

        List<IOrder> _activeOrders = new List<IOrder>();
        List<IOrder> _closedOrders = new List<IOrder>();
        public bool HasActiveOrder => _activeOrders.Count > 0;

        public string MarketCode => throw new NotImplementedException();
        public decimal MinimumOrderSize => throw new NotImplementedException();
        public decimal BestAskPrice => throw new NotImplementedException();
        public decimal BestBidPrice => throw new NotImplementedException();
        public decimal? TotalPositionSize => throw new NotImplementedException();
        public IPositions Positions => throw new NotImplementedException();

        public event EventHandler<PositionEventArgs> PositionChanged;
        public event EventHandler<OrderTransactionEventArgs> OrderTransactionChanged;

        IAccount _account;

        public TestMarket()
        {
        }

        public TestMarket(TestAccount account, string marketSymbol)
        {
            _account = account;
            MarketSymbol = marketSymbol;
            account.RegisterMarket(marketSymbol, this);
        }

        public void Dispose()
        {
        }

        public IOrderTransaction PlaceOrder(IOrder order)
        {
            if (order is TestOrder to)
            {
                to.Open(LastUpdatedTime);
                if (!to.TryExecute(LastUpdatedTime, LastTradedPrice))
                {
                    _activeOrders.Add(order);
                    return default;
                }

                if (to.IsClosed)
                {
                    _closedOrders.Add(order);
                }
                else
                {
                    _activeOrders.Add(order);
                }
            }

            //OrderChanged?.Invoke(order);

            return default;
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public IOrderTransaction PlaceOrder(IOrder order, TimeSpan timeToExpire, TimeInForce timeInForce)
        {
            throw new NotImplementedException();
        }

        public IObservable<ITicker> GetTickerSource()
        {
            throw new NotImplementedException();
        }
    }
}
