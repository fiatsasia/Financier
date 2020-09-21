//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financier.Trading
{
    public class OrderFactory
    {
        // Market price
        public virtual IOrder MarketPrice(decimal size) => Order.MarketPrice(size);

        // Limit price
        public virtual IOrder LimitPrice(decimal price, decimal size) => Order.LimitPrice(price, size);
        public virtual IOrder LimitPrice(OrderPriceType orderPriceType, decimal size) => Order.LimitPrice(orderPriceType, size);
        public virtual IOrder LimitPrice(OrderPriceType orderPriceType, decimal orderPriceOffset, decimal size) => Order.LimitPrice(orderPriceType, orderPriceOffset, size);

        // Stop loss
        public virtual IOrder StopLoss(decimal triggerPrice, decimal size) => Order.StopLoss(triggerPrice, size);
        public virtual IOrder StopLoss(OrderPriceType triggerPriceType, decimal triggerPriceOffset, decimal size) => Order.StopLoss(triggerPriceType, triggerPriceOffset, size);

        // Stop loss limit
        public virtual IOrder StopLossLimit(decimal triggerPrice, decimal orderPrice, decimal size) => Order.StopLossLimit(triggerPrice, orderPrice, size);
        public virtual IOrder StopLossLimit(OrderPriceType triggerPriceType, decimal triggerPriceOffset, OrderPriceType orderPriceType, decimal orderPriceOffset, decimal size) => Order.StopLossLimit(triggerPriceType, triggerPriceOffset, orderPriceType, orderPriceOffset, size);

        // Trailing stop
        public virtual IOrder TrailingStop(decimal trailingOffset, decimal size) => Order.TrailingStop(trailingOffset, size);

        // Trailing stop limit
        public virtual IOrder TrailingStopLimit(decimal trailingOffset, decimal orderPriceOffset, decimal size) => Order.TrailingStopLimit(trailingOffset, orderPriceOffset, size);

        // Conditional orders
        public virtual IOrder IFD(IOrder ifOrder, IOrder doneOrder) => Order.IFD(ifOrder, doneOrder);
        public virtual IOrder OCO(IOrder first, IOrder second) => Order.OCO(first, second);
        public virtual IOrder IFDOCO(IOrder ifOrder, IOrder first, IOrder second) => Order.IFDOCO(ifOrder, first, second);

        // Fundamental operations
        public virtual IOrder TriggerPriceBelow(OrderPriceType triggerPriceType, decimal triggerPrice, IOrder order) => Order.TriggerPriceBelow(triggerPriceType, triggerPrice, order);
        public virtual IOrder TriggerPriceAbove(OrderPriceType triggerPriceType, decimal triggerPrice, IOrder order) => Order.TriggerPriceAbove(triggerPriceType, triggerPrice, order);
        public virtual IOrder TriggerEvent(IOrder order, OrderTransactionEventType eventType, IOrder chainedOrder) => Order.TriggerEvent(order, eventType, chainedOrder);

#if false
        // Price trigger operations

        // Price triggered order operations
        public virtual IOrder StopLoss(decimal triggerPrice, decimal size)
            => (size > 0m)
                ? TriggerPriceAbove(OrderPriceType.BestAsk, triggerPrice, MarketPrice(size))
                : TriggerPriceBelow(OrderPriceType.BestBid, triggerPrice, MarketPrice(size));

        public virtual IOrder StopLossLimit(decimal triggerPrice, decimal orderPrice, decimal size)
            => (size > 0m)
                ? TriggerPriceAbove(OrderPriceType.BestAsk, triggerPrice, LimitPrice(orderPrice, size))
                : TriggerPriceBelow(OrderPriceType.BestBid, triggerPrice, LimitPrice(orderPrice, size));

        // Event triggered order operations
        public virtual IOrder IFD(IOrder ifOrder, IOrder doneOrder) => EventTrigger(ifOrder, OrderTransactionEventType.Executed, doneOrder);
#endif
    }
}
