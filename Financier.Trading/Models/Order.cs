//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financier.Trading
{
    public class Order : IOrder
    {
        #region Order builder
        public static IOrder MarketPrice(decimal size) => new Order { OrderType = OrderType.MarketPrice, OrderSize = size };

        public static IOrder LimitPrice(decimal price, decimal size) => new Order { OrderType = OrderType.LimitPrice, OrderPrice = price, OrderSize = size };
        public static IOrder LimitPrice(OrderPriceType orderPriceType, decimal size) => new Order { OrderType = OrderType.LimitPrice, OrderPriceType = orderPriceType, OrderSize = size };
        public static IOrder LimitPrice(OrderPriceType orderPriceType, decimal orderPriceOffset, decimal size)
            => new Order { OrderType = OrderType.LimitPrice, OrderPriceType = orderPriceType, OrderPriceOffset = orderPriceOffset, OrderSize = size };

        public static IOrder StopLoss(decimal triggerPrice, decimal size) => new Order { OrderType = OrderType.StopLoss, TriggerPrice = triggerPrice, OrderSize = size };
        public static IOrder StopLoss(OrderPriceType triggerPriceType, decimal size)
            => new Order { OrderType = OrderType.StopLoss, TriggerPriceType = triggerPriceType, OrderSize = size };
        public static IOrder StopLoss(OrderPriceType triggerPriceType, decimal triggerPriceOffset, decimal size)
            => new Order { OrderType = OrderType.StopLoss, TriggerPriceType = triggerPriceType, TriggerPriceOffset = triggerPriceOffset, OrderSize = size };
        public static IOrder StopLoss(OrderPriceType referencePriceType, OrderPriceType triggerPriceType, decimal triggerPriceOffset, decimal size)
            => new Order { OrderType = OrderType.StopLoss, ReferencePriceType = referencePriceType, TriggerPriceType = triggerPriceType, TriggerPriceOffset = triggerPriceOffset, OrderSize = size };

        public static IOrder StopLossLimit(decimal triggerPrice, decimal orderPrice, decimal size)
            => new Order { OrderType = OrderType.StopLossLimit, TriggerPrice = triggerPrice, OrderPrice = orderPrice, OrderSize = size };
        public static IOrder StopLossLimit(OrderPriceType triggerPriceType, decimal triggerPriceOffset, OrderPriceType orderPriceType, decimal orderPriceOffset, decimal size)
            => new Order { OrderType = OrderType.StopLossLimit, TriggerPriceType = triggerPriceType, TriggerPriceOffset = triggerPriceOffset, OrderPriceType = orderPriceType, OrderPriceOffset = orderPriceOffset, OrderSize = size };
        public static IOrder StopLossLimit(OrderPriceType referencePriceType, OrderPriceType triggerPriceType, decimal triggerPriceOffset, OrderPriceType orderPriceType, decimal orderPriceOffset, decimal size)
            => new Order { OrderType = OrderType.StopLossLimit, ReferencePriceType = referencePriceType, TriggerPriceType = triggerPriceType, TriggerPriceOffset = triggerPriceOffset, OrderPriceType = orderPriceType, OrderPriceOffset = orderPriceOffset, OrderSize = size };

        public static IOrder TrailingStop(decimal trailingOffset, decimal size)
            => new Order { OrderType = OrderType.TrailingStop, TrailingOffset = trailingOffset, OrderSize = size };

        public static IOrder TrailingStopLimit(decimal trailingOffset, decimal orderPriceOffset, decimal size)
            => new Order { OrderType = OrderType.TrailingStopLimit, TrailingOffset = trailingOffset, OrderPriceOffset = orderPriceOffset, OrderSize = size };

        public static IOrder IFD(IOrder ifOrder, IOrder doneOrder) => new Order { OrderType = OrderType.IFD, Children = new List<IOrder> { ifOrder, doneOrder } };
        public static IOrder OCO(IOrder first, IOrder second) => new Order { OrderType = OrderType.OCO, Children = new List<IOrder> { first, second } };
        public static IOrder IFDOCO(IOrder ifOrder, IOrder first, IOrder second) => new Order { OrderType = OrderType.IFDOCO, Children = new List<IOrder> { ifOrder, first, second } };

        // Foundamental operations
        public static IOrder TriggerPriceBelow(OrderPriceType triggerPriceType, decimal triggerPrice, IOrder order)
            => new Order { OrderType = OrderType.TriggerPriceBelow, TriggerPriceType = triggerPriceType, TriggerPrice = triggerPrice, Children = new List<IOrder> { order } };

        public static IOrder TriggerPriceAbove(OrderPriceType triggerPriceType, decimal triggerPrice, IOrder order)
            => new Order { OrderType = OrderType.TriggerPriceAbove, TriggerPriceType = triggerPriceType, TriggerPrice = triggerPrice, Children = new List<IOrder> { order } };

        public static IOrder TriggerEvent(IOrder order, OrderTransactionEventType eventType, IOrder chainedOrder)
            => new Order { OrderType = OrderType.TriggerEvent, TriggerEventType = eventType, Children = new List<IOrder> { order, chainedOrder } };


        // WIP
        public static IOrder StopAndReverse(IPosition openPosition) => throw new NotSupportedException();
        public static IOrder StopAndReverse(IAsset openAsset) => throw new NotSupportedException();
        #endregion Order builder

        public OrderType OrderType { get; internal set; }
        public virtual decimal? OrderSize { get; internal set; }
        public virtual decimal? OrderPrice { get; internal set; }

        public virtual decimal? TriggerPrice { get; internal set; }
        public virtual decimal? TrailingOffset { get; internal set; }

        public virtual IReadOnlyList<IOrder> Children { get; internal set; }

        public OrderPriceType OrderPriceType { get; internal set; }
        public decimal OrderPriceOffset { get; internal set; }
        public OrderPriceType TriggerPriceType { get; internal set; }
        public decimal TriggerPriceOffset { get; internal set; }
        public OrderPriceType ReferencePriceType { get; internal set; }
        public OrderTransactionEventType TriggerEventType { get; internal set; }

        //
        public virtual DateTime? OpenTime { get; internal set; }
        public virtual DateTime? CloseTime { get; internal set; }
        public virtual OrderState State { get; internal set; }

        public virtual decimal? ExecutedPrice => throw new NotImplementedException();
        public virtual decimal? ExecutedSize => throw new NotImplementedException();
        public virtual IEnumerable<IExecution> Executions => throw new NotImplementedException();

        public virtual bool IsClosed => State == OrderState.Completed;

        public Order()
        {
        }
    }
}
