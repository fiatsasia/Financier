//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financier.Trading
{
    public class Market : IMarket, IDisposable
    {
        public string MarketSymbol { get; private set; }
        public decimal MarketPrice { get; private set; }
        public DateTime LastUpdatedTime { get; private set; }

        List<IOrder> _activeOrders = new List<IOrder>();
        List<IOrder> _closedOrders = new List<IOrder>();
        public bool HasActiveOrder => _activeOrders.Count > 0;

        public event Action<IOrder> OrderChanged;

        IAccount _account;

        public Market()
        {
        }

        public Market(Account account, string marketSymbol)
        {
            _account = account;
            MarketSymbol = marketSymbol;
            account.RegisterMarket(marketSymbol, this);
        }

        public void Dispose()
        {
        }

        public virtual void UpdatePrice(DateTime time, decimal price)
        {
            LastUpdatedTime = time;
            MarketPrice = price;

            var activeOrders = new List<IOrder>();
            foreach (var order in _activeOrders)
            {
                if (!order.TryExecute(LastUpdatedTime, MarketPrice))
                {
                    continue;
                }

                if (order.IsClosed)
                {
                    _closedOrders.Add(order);
                }
                else
                {
                    activeOrders.Add(order);
                }
                OrderChanged?.Invoke(order);
            }
            _activeOrders = activeOrders;
        }

        public bool PlaceOrder(IOrder order)
        {
            order.Open(LastUpdatedTime);

            if (!order.TryExecute(LastUpdatedTime, MarketPrice))
            {
                _activeOrders.Add(order);
                return true;
            }

            if (order.IsClosed)
            {
                _closedOrders.Add(order);
            }
            else
            {
                _activeOrders.Add(order);
            }
            OrderChanged?.Invoke(order);

            return true;
        }

        public async Task PlaceOrderAsync(IOrder order)
        {
            PlaceOrder(order);
            await Task.CompletedTask;
        }


        public bool PlaceOrder(IOrder order, TimeInForce tif)
        {
            throw new NotSupportedException();
        }

        OrderFactory _orderFactory = new OrderFactory();
        public IOrderFactory GetOrderFactory() => _orderFactory;
    }
}
