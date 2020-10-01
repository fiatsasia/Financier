//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financier.Trading
{
    public class OrderFactory
    {
        // Basic orders
        public static IOrder MarketPrice(decimal size) => new Order { OrderType = OrderType.MarketPrice, OrderSize = size };
        public static IOrder LimitPrice(decimal price, decimal size) => new Order { OrderType = OrderType.LimitPrice, OrderPrice = price, OrderSize = size };

        // Simple conditional orders
        public static IOrder StopLoss(decimal triggerPrice, decimal size) => new Order { OrderType = OrderType.StopLoss, TriggerPrice = triggerPrice, OrderSize = size };
        public static IOrder StopLimit(decimal triggerPrice, decimal stopPrice, decimal size) => new Order { OrderType = OrderType.StopLimit, TriggerPrice = triggerPrice, OrderPrice = stopPrice, OrderSize = size };
        public static IOrder TrailingStop(decimal trailingOffset, decimal size) => new Order { OrderType = OrderType.TrailingStop, TrailingOffset = trailingOffset, OrderSize = size };
        public static IOrder TrailingStopLimit(decimal trailingOffset, decimal stopPrice, decimal size) => new Order { OrderType = OrderType.TrailingStopLimit, TrailingOffset = trailingOffset, OrderPrice = stopPrice, OrderSize = size };
        public static IOrder TakeProfit(decimal profitPrice, decimal size) => new Order { OrderType = OrderType.TakeProfit, ProfitPrice = profitPrice, OrderSize = size };
        public static IOrder TakeProfitLimit(decimal profitPrice, decimal limitPrice, decimal size) => new Order { OrderType = OrderType.TakeProfitLimit, ProfitPrice = profitPrice, OrderPrice = limitPrice, OrderSize = size };

        // Combined conditional orders
        public static IOrder IFD(IOrder ifOrder, IOrder doneOrder) => new Order(new IOrder[] { ifOrder, doneOrder }) { OrderType = OrderType.IFD };
        public static IOrder OCO(IOrder first, IOrder second) => new Order(new IOrder[] { first, second }) { OrderType = OrderType.OCO };
        public static IOrder IFDOCO(IOrder ifOrder, IOrder first, IOrder second) => new Order(new IOrder[] { ifOrder, first, second }) { OrderType = OrderType.IFDOCO };
    }
}
