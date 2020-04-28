//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Linq;

namespace Financial.Extensions
{
    public class TradingMarket<TPrice, TSize> : ITradingMarket<TPrice, TSize>
    {
        public string MarketSymbol { get; private set; }
        public TPrice MarketPrice { get; private set; }
        public DateTime LastUpdatedTime { get; private set; }

        List<ITradingOrder<TPrice, TSize>> _activeOrders = new List<ITradingOrder<TPrice, TSize>>();
        List<ITradingOrder<TPrice, TSize>> _closedOrders = new List<ITradingOrder<TPrice, TSize>>();
        public bool HasActiveOrder => _activeOrders.Count > 0;

        List<ITradingPosition<TPrice, TSize>> _openPositions = new List<ITradingPosition<TPrice, TSize>>();
        public bool HasOpenPosition => _openPositions.Count > 0;


        public event Action<ITradingPosition<TPrice, TSize>> PositionChanged;

        public virtual void UpdatePrice(DateTime time, TPrice price)
        {
            LastUpdatedTime = time;
            MarketPrice = price;

            foreach (var order in _activeOrders)
            {
                if (order.CanExecute(price))
                {
                    if (order.HasChildOrder)
                    {
                        foreach (var child in order.Children)
                        {
                            PlaceOrder(child); // Call recursive
                        };
                        return;
                    }

                    ExecuteOrder(order);
                    _activeOrders.Remove(order);
                    _closedOrders.Add(order);
                }
            }
        }

        public void PlaceOrder(ITradingOrder<TPrice, TSize> order)
        {
            order.Open(LastUpdatedTime);

            if (order.CanExecute(MarketPrice))
            {
                if (order.HasChildOrder)
                {
                    foreach (var child in order.Children)
                    {
                        PlaceOrder(child); // Call recursive
                    };
                    return;
                }

                ExecuteOrder(order);
                _closedOrders.Add(order);
            }
            else
            {
                _activeOrders.Add(order);
            }
        }

        void ExecuteOrder(ITradingOrder<TPrice, TSize> order)
        {
            ITradingPosition<TPrice, TSize> pos;
            if (order.IsSettlmentOrder)
            {
                pos = order.Position;
                pos.Close(LastUpdatedTime, MarketPrice);
                _openPositions.Remove(pos);
            }
            else // 新規注文の場合
            {
                pos = new TradingPosition<TPrice, TSize>(this);
                pos.Open(LastUpdatedTime, MarketPrice, order.OrderSize);
                _openPositions.Add(pos);
            }

            order.Execute(LastUpdatedTime, MarketPrice);
            PositionChanged?.Invoke(pos);
        }

        TradingOrderFactory<TPrice, TSize> _orderFactory = new TradingOrderFactory<TPrice, TSize>();
        public ITradingOrderFactory<TPrice, TSize> GetTradeOrderFactory() => _orderFactory;

        public TradingMarket()
        {
        }

        public TradingMarket(string marketSymbol)
        {
            MarketSymbol = marketSymbol;
        }
    }
}
