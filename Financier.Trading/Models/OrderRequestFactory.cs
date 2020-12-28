//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading
{
    public class OrderRequestFactory<TOrderRequest> where TOrderRequest : IOrderRequest, new()
    {
        // Basic orders
        public TOrderRequest Market(decimal size) => new TOrderRequest { OrderType = OrderType.Market, OrderSize = size };
        public TOrderRequest Limit(decimal price, decimal size) => new TOrderRequest { OrderType = OrderType.Limit, OrderPrice = price, OrderSize = size };

        // Simple conditional orders
        public TOrderRequest Stop(decimal triggerPrice, decimal size) => new TOrderRequest { OrderType = OrderType.Stop, TriggerPrice = triggerPrice, OrderSize = size };
        public TOrderRequest StopLimit(decimal triggerPrice, decimal stopPrice, decimal size) => new TOrderRequest { OrderType = OrderType.StopLimit, TriggerPrice = triggerPrice, StopPrice = stopPrice, OrderSize = size };
        public TOrderRequest TrailingStop(decimal trailingOffset, decimal size) => new TOrderRequest { OrderType = OrderType.TrailingStop, TrailingOffset = trailingOffset, OrderSize = size };
        public TOrderRequest TrailingStopLimit(decimal trailingOffset, decimal stopPrice, decimal size) => new TOrderRequest { OrderType = OrderType.TrailingStopLimit, TrailingOffset = trailingOffset, StopPrice = stopPrice, OrderSize = size };
        public TOrderRequest TakeProfit(decimal profitPrice, decimal size) => new TOrderRequest { OrderType = OrderType.TakeProfit, ProfitPrice = profitPrice, OrderSize = size };

        // Combined conditional orders
        public TOrderRequest IFD(IOrderRequest ifOrder, IOrderRequest doneOrder) => new TOrderRequest { OrderType = OrderType.IFD, Children = new IOrderRequest[] { ifOrder, doneOrder } };
        public TOrderRequest OCO(IOrderRequest first, IOrderRequest second) => new TOrderRequest { OrderType = OrderType.OCO, Children = new IOrderRequest[] { first, second } };
        public TOrderRequest IFDOCO(IOrderRequest ifOrder, IOrderRequest first, IOrderRequest second) => new TOrderRequest { OrderType = OrderType.IFDOCO, Children = new IOrderRequest[] { ifOrder, first, second } };
    }
}
