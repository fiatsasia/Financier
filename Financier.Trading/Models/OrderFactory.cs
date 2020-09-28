//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;

namespace Financier.Trading
{
    public class OrderFactory
    {
        // Basic orders
        public static IOrder MarketPrice(decimal size) => new Order { OrderType = OrderType.MarketPrice, OrderSize = size };
        public static IOrder LimitPrice(decimal price, decimal size) => new Order { OrderType = OrderType.LimitPrice, OrderPrice = price, OrderSize = size };

        // Simple conditional orders
        public static IOrder StopLoss(decimal triggerPrice, decimal size) => new Order { OrderType = OrderType.StopLoss, TriggerPrice = triggerPrice, OrderSize = size };
        public static IOrder StopLimit(decimal triggerPrice, decimal orderPrice, decimal size) => new Order { OrderType = OrderType.StopLimit, TriggerPrice = triggerPrice, OrderPrice = orderPrice, OrderSize = size };
        public static IOrder TrailingStop(decimal trailingOffset, decimal size) => new Order { OrderType = OrderType.TrailingStop, TrailingOffset = trailingOffset, OrderSize = size };
        public static IOrder TrailingStopLimit(decimal trailingOffset, decimal orderPriceOffset, decimal size) => new Order { OrderType = OrderType.TrailingStopLimit, TrailingOffset = trailingOffset, OrderPriceOffset = orderPriceOffset, OrderSize = size };

        // Combined conditional orders
        public static IOrder IFD(IOrder ifOrder, IOrder doneOrder) => new Order(new IOrder[] { ifOrder, doneOrder }) { OrderType = OrderType.IFD };
        public static IOrder OCO(IOrder first, IOrder second) => new Order(new IOrder[] { first, second }) { OrderType = OrderType.OCO };
        public static IOrder IFDOCO(IOrder ifOrder, IOrder first, IOrder second) => new Order(new IOrder[] { ifOrder, first, second }) { OrderType = OrderType.IFDOCO };

        // Fundamental operations
        static IOrder TriggerPriceBelow(decimal triggerPrice, IOrder order) => new Order(order) { OrderType = OrderType.TriggerPriceBelow, TriggerPriceType = OrderPriceType.LastTraded, TriggerPrice = triggerPrice };
        static IOrder TriggerPriceAbove(decimal triggerPrice, IOrder order) => new Order(order) { OrderType = OrderType.TriggerPriceAbove, TriggerPriceType = OrderPriceType.LastTraded, TriggerPrice = triggerPrice };
        static IOrder TriggerOffsetPrice(decimal triggerPrice, decimal triggerPriceOffset, IOrder order) => new Order(order) { OrderType = OrderType.TriggerPriceOffset, TriggerPrice = triggerPrice, TriggerPriceOffset = triggerPriceOffset };
        static IOrder TriggerEvent(IOrder order, OrderTransactionEventType eventType, IOrder chainedOrder) => new Order(new IOrder[] { order, chainedOrder }) { OrderType = OrderType.TriggerEvent, TriggerEventType = eventType };

        // WIP
        public static IOrder StopAndReverse(IPosition openPosition) => throw new NotSupportedException();
        public static IOrder StopAndReverse(IAsset openAsset) => throw new NotSupportedException();

        // Translate conditional order to fundamental operations
        public static IOrder Translate(IOrder order)
        {
            switch (order.OrderType)
            {
                case OrderType.IFD: // Executed trigger + order
                    return TriggerEvent(order.Children[0], OrderTransactionEventType.Executed, order.Children[1]);

                case OrderType.IFDOCO: // IFD + OCO
                    return TriggerEvent(order.Children[0], OrderTransactionEventType.Executed, new Order(order.Children.Skip(1)) { OrderType = OrderType.OCO });

                case OrderType.StopLoss:
                    return (order.OrderSize > 0m)
                        ? TriggerPriceAbove(order.TriggerPrice.Value, MarketPrice(order.OrderSize.Value))
                        : TriggerPriceBelow(order.TriggerPrice.Value, MarketPrice(order.OrderSize.Value));

                case OrderType.StopLimit:
                    return (order.OrderSize > 0m)
                        ? TriggerPriceAbove(order.TriggerPrice.Value, LimitPrice(order.OrderPrice.Value, order.OrderSize.Value))
                        : TriggerPriceBelow(order.TriggerPrice.Value, LimitPrice(order.OrderPrice.Value, order.OrderSize.Value));

                case OrderType.TrailingStop:
                    return TriggerOffsetPrice(order.TriggerPrice.Value, order.TriggerPriceOffset, MarketPrice(order.OrderSize.Value));

                case OrderType.TrailingStopLimit:
                    return TriggerOffsetPrice(order.TriggerPrice.Value, order.TriggerPriceOffset, LimitPrice(order.OrderPrice.Value, order.OrderSize.Value));

                default:
                    return order;
            }
        }
    }
}
