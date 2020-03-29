//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public class TradingOrderModel : ITradingSimpleOrder
    {
        public TradingOrderType OrderType { get; }

        public decimal OrderPrice { get; }
        public decimal OrderSize { get; }
        public decimal StopTriggerPrice { get; }
        public decimal TrailingStopOffset { get; }

        public DateTime OpenTime { get; }
        public DateTime CloseTime { get; }
        public TradingOrderState Status { get; private set; } = TradingOrderState.New;

        public TradingOrderModel() { }

        public TradingOrderModel(TradingOrderType orderType, TradeSide side, decimal size)
        {
            OrderType = orderType;
            OrderSize = side == TradeSide.Buy ? size : -size;
        }

        public TradingOrderModel(TradingOrderType orderType, TradeSide side, decimal price, decimal size)
        {
            OrderType = orderType;
            OrderPrice = price;
            OrderSize = side == TradeSide.Buy ? size : -size;
        }
    }
}
