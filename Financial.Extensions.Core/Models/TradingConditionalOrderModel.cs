//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financial.Extensions
{
    public class TradingConditionalOrderModel : ITradingConditionalOrder
    {
        public TradeConditionalOrderType OrderType { get; }

        public DateTime OpenTime { get; }
        public DateTime CloseTime { get; }
        public TradingOrderState Status { get; }

        List<ITradingOrder> _childOrders;
        public IReadOnlyList<ITradingOrder> ChildOrders => _childOrders;
        public ITradingSimpleOrder CurrentOrder { get; private set; }

        public TradingConditionalOrderModel(TradeConditionalOrderType orderType, params ITradingOrder[] orders)
        {
            OrderType = orderType;
            _childOrders = new List<ITradingOrder>(orders);
        }
    }
}
