//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public class FxTradingOrderModel : IFxTradingSimpleOrder
    {
        public Guid OrderId { get; }
        public FxTradingOrderType OrderType { get; }

        public decimal OrderPrice { get; }
        public decimal OrderSize { get; }
        public decimal StopTriggerPrice { get; }
        public decimal TrailingStopOffset { get; }

        public DateTime OpenTime { get; }
        public DateTime CloseTime { get; }
        public FxTradingOrderState Status { get; private set; } = FxTradingOrderState.New;
        public object Tag { get; set; }

        public event Action<IFxTradingSimpleOrder> OrderChanged;

        public FxTradingOrderModel() { }

        public FxTradingOrderModel(FxTradingOrderType orderType, FxTradeSide side, decimal size)
        {
            OrderId = Guid.NewGuid();
            OrderType = orderType;
            OrderSize = side == FxTradeSide.Buy ? size : -size;
        }

        public FxTradingOrderModel(FxTradingOrderType orderType, FxTradeSide side, decimal price, decimal size)
        {
            OrderId = Guid.NewGuid();
            OrderType = orderType;
            OrderPrice = price;
            OrderSize = side == FxTradeSide.Buy ? size : -size;
        }

        public virtual void CancelOrder()
        {
            switch (Status)
            {
                case FxTradingOrderState.New:
                case FxTradingOrderState.PartiallyFilled:
                    // キャンセル可能
                    break;
            }
        }
    }
}
