//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;

namespace Financier.Trading
{
    public static class OrderFactory
    {
        static decimal SideToSize(TradeSide side, decimal size) => side switch { TradeSide.Buy => size, TradeSide.Sell => -size, _ => throw new ArgumentException() };

        public static Order Market(decimal size)
        {
            if (size == decimal.Zero)
            {
                throw new ArgumentOutOfRangeException();
            }
            return new Order(OrderType.Market) { OrderSize = size };
        }

        public static Order Market(TradeSide side, decimal size)
        {
            if (size <= decimal.Zero)
            {
                throw new ArgumentOutOfRangeException();
            }
            return new Order(OrderType.Market) { OrderSize = SideToSize(side, size) };
        }

        public static Order Limit(decimal price, decimal size)
        {
            if (price <= decimal.Zero || size == decimal.Zero)
            {
                throw new ArgumentOutOfRangeException();
            }
            return new Order(OrderType.Limit) { OrderPrice = price, OrderSize = size };
        }

        public static Order Limit(TradeSide side, decimal price, decimal size)
        {
            if (price <= decimal.Zero || size <= decimal.Zero)
            {
                throw new ArgumentOutOfRangeException();
            }
            return new Order(OrderType.Limit) { OrderPrice = price, OrderSize = SideToSize(side, size) };
        }

        public static Order Stop(decimal triggerPrice, decimal size)
            => new Order(OrderType.Stop) { TriggerPrice = triggerPrice, OrderSize = size };

        public static Order Stop(TradeSide side, decimal triggerPrice, decimal size)
            => new Order(OrderType.Stop) { TriggerPrice = triggerPrice, OrderSize = SideToSize(side, size) };

        public static Order StopLimit(decimal triggerPrice, decimal stopPrice, decimal size)
            => new Order(OrderType.StopLimit) { TriggerPrice = triggerPrice, StopPrice = stopPrice, OrderSize = size };

        public static Order StopLimit(TradeSide side, decimal triggerPrice, decimal stopPrice, decimal size)
            => new Order(OrderType.StopLimit) { TriggerPrice = triggerPrice, StopPrice = stopPrice, OrderSize = SideToSize(side, size) };

        public static Order TrailingStop(decimal trailingOffset, decimal size)
            => new Order(OrderType.TrailingStop) { TrailingOffset = trailingOffset, OrderSize = size };

        public static Order TrailingStop(TradeSide side, decimal trailingOffset, decimal size)
            => new Order(OrderType.TrailingStop) { TrailingOffset = trailingOffset, OrderSize = SideToSize(side, size) };

        public static Order TrailingStopLimit(TradeSide side, decimal trailingOffset, decimal stopPrice, decimal size)
            => new Order(OrderType.TrailingStopLimit) { TrailingOffset = trailingOffset, StopPrice = stopPrice, OrderSize = SideToSize(side, size) };

        public static Order TakeProfit(TradeSide side, decimal profitPrice, decimal size)
            => new Order(OrderType.TakeProfit) { ProfitPrice = profitPrice, OrderSize = SideToSize(side, size) };

        public static Order IFD(Order ifOrder, Order doneOrder) => new Order(OrderType.IFD, new Order[] { ifOrder, doneOrder });

        public static Order OCO(Order first, Order second) => new Order(OrderType.OCO, new Order[] { first, second });

        public static Order IFDOCO(Order ifOrder, Order first, Order second) => new Order(OrderType.IFD, new Order[] { ifOrder, first, second });
    }
}
