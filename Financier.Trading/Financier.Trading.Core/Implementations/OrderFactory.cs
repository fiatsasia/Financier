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

        public static OrderRequest Market(decimal size)
        {
            if (size == decimal.Zero)
            {
                throw new ArgumentOutOfRangeException();
            }
            return new OrderRequest(OrderType.Market) { OrderSize = size };
        }

        public static OrderRequest Market(TradeSide side, decimal size)
        {
            if (size <= decimal.Zero)
            {
                throw new ArgumentOutOfRangeException();
            }
            return new OrderRequest(OrderType.Market) { OrderSize = SideToSize(side, size) };
        }

        public static OrderRequest Limit(decimal price, decimal size)
        {
            if (price <= decimal.Zero || size == decimal.Zero)
            {
                throw new ArgumentOutOfRangeException();
            }
            return new OrderRequest(OrderType.Limit) { OrderPrice = price, OrderSize = size };
        }

        public static OrderRequest Limit(TradeSide side, decimal price, decimal size)
        {
            if (price <= decimal.Zero || size <= decimal.Zero)
            {
                throw new ArgumentOutOfRangeException();
            }
            return new OrderRequest(OrderType.Limit) { OrderPrice = price, OrderSize = SideToSize(side, size) };
        }

        public static OrderRequest Stop(decimal triggerPrice, decimal size)
            => new OrderRequest(OrderType.Stop) { TriggerPrice = triggerPrice, OrderSize = size };

        public static OrderRequest Stop(TradeSide side, decimal triggerPrice, decimal size)
            => new OrderRequest(OrderType.Stop) { TriggerPrice = triggerPrice, OrderSize = SideToSize(side, size) };

        public static OrderRequest StopLimit(decimal triggerPrice, decimal stopPrice, decimal size)
            => new OrderRequest(OrderType.StopLimit) { TriggerPrice = triggerPrice, StopPrice = stopPrice, OrderSize = size };

        public static OrderRequest StopLimit(TradeSide side, decimal triggerPrice, decimal stopPrice, decimal size)
            => new OrderRequest(OrderType.StopLimit) { TriggerPrice = triggerPrice, StopPrice = stopPrice, OrderSize = SideToSize(side, size) };

        public static OrderRequest TrailingStop(decimal trailingOffset, decimal size)
            => new OrderRequest(OrderType.TrailingStop) { TrailingOffset = trailingOffset, OrderSize = size };

        public static OrderRequest TrailingStop(TradeSide side, decimal trailingOffset, decimal size)
            => new OrderRequest(OrderType.TrailingStop) { TrailingOffset = trailingOffset, OrderSize = SideToSize(side, size) };

        public static OrderRequest TrailingStopLimit(TradeSide side, decimal trailingOffset, decimal stopPrice, decimal size)
            => new OrderRequest(OrderType.TrailingStopLimit) { TrailingOffset = trailingOffset, StopPrice = stopPrice, OrderSize = SideToSize(side, size) };

        public static OrderRequest TakeProfit(TradeSide side, decimal profitPrice, decimal size)
            => new OrderRequest(OrderType.TakeProfit) { ProfitPrice = profitPrice, OrderSize = SideToSize(side, size) };

        public static OrderRequest IFD(OrderRequest ifOrder, OrderRequest doneOrder) => new OrderRequest(OrderType.IFD, new OrderRequest[] { ifOrder, doneOrder });
        public static OrderRequest OCO(OrderRequest first, OrderRequest second) => new OrderRequest(OrderType.OCO, new OrderRequest[] { first, second });
        public static OrderRequest IFDOCO(OrderRequest ifOrder, OrderRequest first, OrderRequest second) => new OrderRequest(OrderType.IFD, new OrderRequest[] { ifOrder, first, second });
    }
}
