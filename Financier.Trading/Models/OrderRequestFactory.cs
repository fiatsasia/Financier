//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading
{
    public class OrderRequestFactory<TOrderRequest> where TOrderRequest : IOrderRequest<TOrderRequest>, new()
    {
        // Basic orders
        public virtual TOrderRequest Market(decimal size) => new TOrderRequest
        {
            OrderType = OrderType.Market,
            OrderSize = size
        };

        public virtual TOrderRequest Market(string productCode, decimal size) => new TOrderRequest
        {
            ProductCode = productCode,
            OrderType = OrderType.Market,
            OrderSize = size
        };

        public virtual TOrderRequest Limit(decimal price, decimal size) => new TOrderRequest
        {
            OrderType = OrderType.Limit,
            OrderPrice = price,
            OrderSize = size
        };

        public virtual TOrderRequest Limit(string productCode, decimal price, decimal size) => new TOrderRequest
        {
            ProductCode = productCode,
            OrderType = OrderType.Limit,
            OrderPrice = price,
            OrderSize = size
        };

        // Simple conditional orders
        public virtual TOrderRequest Stop(decimal triggerPrice, decimal size) => new TOrderRequest { OrderType = OrderType.Stop, TriggerPrice = triggerPrice, OrderSize = size };
        public virtual TOrderRequest StopLimit(decimal triggerPrice, decimal stopPrice, decimal size) => new TOrderRequest { OrderType = OrderType.StopLimit, TriggerPrice = triggerPrice, StopPrice = stopPrice, OrderSize = size };
        public virtual TOrderRequest TrailingStop(decimal trailingOffset, decimal size) => new TOrderRequest { OrderType = OrderType.TrailingStop, TrailingOffset = trailingOffset, OrderSize = size };
        public virtual TOrderRequest TrailingStopLimit(decimal trailingOffset, decimal stopPrice, decimal size) => new TOrderRequest { OrderType = OrderType.TrailingStopLimit, TrailingOffset = trailingOffset, StopPrice = stopPrice, OrderSize = size };
        public virtual TOrderRequest TakeProfit(decimal profitPrice, decimal size) => new TOrderRequest { OrderType = OrderType.TakeProfit, ProfitPrice = profitPrice, OrderSize = size };

        // Combined conditional orders
        public virtual TOrderRequest IFD(TOrderRequest ifOrder, TOrderRequest doneOrder) => new TOrderRequest { OrderType = OrderType.IFD, Children = new TOrderRequest[] { ifOrder, doneOrder } };
        public virtual TOrderRequest OCO(TOrderRequest first, TOrderRequest second) => new TOrderRequest { OrderType = OrderType.OCO, Children = new TOrderRequest[] { first, second } };
        public virtual TOrderRequest IFDOCO(TOrderRequest ifOrder, TOrderRequest first, TOrderRequest second) => new TOrderRequest { OrderType = OrderType.IFDOCO, Children = new TOrderRequest[] { ifOrder, first, second } };
    }
}
