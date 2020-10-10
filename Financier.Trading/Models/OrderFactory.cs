//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financier.Trading
{
    public class OrderFactory : IOrderFactory
    {
        // Basic orders
        public virtual IOrder MarketPrice(decimal size) => Construct(OrderType.LimitPrice, orderSize: size);
        public virtual IOrder LimitPrice(decimal price, decimal size) => Construct(OrderType.LimitPrice, orderPrice: price, orderSize: size);

        // Simple conditional orders
        public virtual IOrder StopLoss(decimal triggerPrice, decimal size) => Construct(OrderType.StopLoss, triggerPrice: triggerPrice, orderSize: size);
        public virtual IOrder StopLimit(decimal triggerPrice, decimal stopPrice, decimal size) => Construct(OrderType.StopLimit, triggerPrice: triggerPrice, orderPrice: stopPrice, orderSize: size);
        public virtual IOrder TrailingStop(decimal trailingOffset, decimal size) => Construct(OrderType.TrailingStop, trailingOffset: trailingOffset, orderSize: size);
        public virtual IOrder TrailingStopLimit(decimal trailingOffset, decimal stopPrice, decimal size) => Construct(OrderType.TrailingStopLimit, trailingOffset: trailingOffset, orderPrice: stopPrice, orderSize: size);
        public virtual IOrder TakeProfit(decimal profitPrice, decimal size) => Construct(OrderType.TakeProfit, profitPrice: profitPrice, orderSize: size);
        public virtual IOrder TakeProfitLimit(decimal profitPrice, decimal limitPrice, decimal size) => Construct(OrderType.TakeProfitLimit, profitPrice: profitPrice, orderPrice: limitPrice, orderSize: size);

        // Combined conditional orders
        public virtual IOrder IFD(IOrder ifOrder, IOrder doneOrder) => Construct(OrderType.IFD, children: new IOrder[] { ifOrder, doneOrder });
        public virtual IOrder OCO(IOrder first, IOrder second) => Construct(OrderType.OCO, children: new IOrder[] { first, second });
        public virtual IOrder IFDOCO(IOrder ifOrder, IOrder first, IOrder second) => Construct(OrderType.IFDOCO, children: new IOrder[] { ifOrder, first, second });

        // Overridable constructor
        protected virtual IOrder Construct(
            OrderType orderType,
            decimal? orderPrice = null,
            decimal? orderSize = null,
            decimal? triggerPrice = null,
            decimal? trailingOffset = null,
            decimal? profitPrice = null,
            IOrder[] children = null
        )
        {
            return new Order(children != null ? children : new IOrder[0])
            {
                OrderType = orderType,
                OrderPrice = orderPrice,
                OrderSize = orderSize,
                TriggerPrice = triggerPrice,
                TrailingOffset = trailingOffset,
                ProfitPrice = profitPrice,
            };
        }
    }
}
