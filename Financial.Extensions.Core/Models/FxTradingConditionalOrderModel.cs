//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financial.Extensions
{
    public class FxTradingConditionalOrderModel : IFxTradingConditionalOrder
    {
        public Guid OrderId { get; }
        public FxTradeConditionalOrderType OrderType { get; }

        public DateTime OpenTime { get; }
        public DateTime CloseTime { get; }
        public FxTradingOrderState Status { get; }
        public object Tag { get; set; }

        List<IFxTradingOrder> _childOrders;
        public IReadOnlyList<IFxTradingOrder> ChildOrders => _childOrders;
        public IFxTradingSimpleOrder CurrentOrder { get; private set; }

        public event Action<IFxTradingSimpleOrder> OrderChanged;

        public FxTradingConditionalOrderModel(FxTradeConditionalOrderType orderType, params IFxTradingOrder[] orders)
        {
            OrderType = orderType;
            _childOrders = new List<IFxTradingOrder>(orders);
        }

        public virtual void CancelOrder()
        {
        }
    }
}
