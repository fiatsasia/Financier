//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Linq;

namespace Financial.Extensions.Trading
{
    public class Market<TPrice, TSize> : IMarket<TPrice, TSize>
    {
        public string MarketSymbol { get; private set; }
        public TPrice MarketPrice { get; private set; }
        public DateTime LastUpdatedTime { get; private set; }

        List<IOrder<TPrice, TSize>> _activeOrders = new List<IOrder<TPrice, TSize>>();
        List<IOrder<TPrice, TSize>> _closedOrders = new List<IOrder<TPrice, TSize>>();
        public bool HasActiveOrder => _activeOrders.Count > 0;

        public event Action<IOrder<TPrice, TSize>> OrderChanged;

        public virtual void UpdatePrice(DateTime time, TPrice price)
        {
            LastUpdatedTime = time;
            MarketPrice = price;

            var activeOrders = new List<IOrder<TPrice, TSize>>();
            foreach (var order in _activeOrders)
            {
                if (order.TryExecute(LastUpdatedTime, MarketPrice))
                {
                    if (order.Status != OrderState.PartiallyFilled)
                    {
                        _closedOrders.Add(order);
                    }
                    else
                    {
                        activeOrders.Add(order);
                    }
                    OrderChanged?.Invoke(order);
                }
                else
                {
                    activeOrders.Add(order);
                }
            }
            _activeOrders = activeOrders;
        }

        public void PlaceOrder(IOrder<TPrice, TSize> order)
        {
            order.Open(LastUpdatedTime);

            if (order.TryExecute(LastUpdatedTime, MarketPrice) && order.Status != OrderState.PartiallyFilled)
            {
                _closedOrders.Add(order);
            }
            else
            {
                _activeOrders.Add(order);
            }
            OrderChanged?.Invoke(order);
        }

        public void PlaceOrder(IOrder<TPrice, TSize> order, TimeInForce tif)
        {
            throw new NotSupportedException();
        }

        OrderFactory<TPrice, TSize> _orderFactory = new OrderFactory<TPrice, TSize>();
        public IOrderFactory<TPrice, TSize> GetTradeOrderFactory() => _orderFactory;

        public Market()
        {
        }

        public Market(string marketSymbol)
        {
            MarketSymbol = marketSymbol;
        }
    }
}
