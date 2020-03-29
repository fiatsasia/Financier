//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financial.Extensions
{
    public interface ITradingOrder
    {
        DateTime OpenTime { get; }
        DateTime CloseTime { get; }
        TradingOrderState Status { get; }
    }

    public interface ITradingSimpleOrder : ITradingOrder
    {
        TradingOrderType OrderType { get; }
        decimal OrderPrice { get; }
        decimal OrderSize { get; }
        decimal StopTriggerPrice { get; }
        decimal TrailingStopOffset { get; }
    }

    public interface ITradingConditionalOrder : ITradingOrder
    {
        TradeConditionalOrderType OrderType { get; }
        IReadOnlyList<ITradingOrder> ChildOrders { get; }
        ITradingSimpleOrder CurrentOrder { get; }
    }

    public abstract class TradingOrderFactoryBase
    {
        public virtual ITradingSimpleOrder CreateMarketPriceOrder(TradeSide side, decimal size) { throw new NotSupportedException(); }
        public virtual ITradingSimpleOrder CreateLimitPriceOrder(TradeSide side, decimal price, decimal size) { throw new NotSupportedException(); }
        public virtual ITradingSimpleOrder CreateStopOrder(decimal size, decimal stopTriggerPrice) { throw new NotSupportedException(); }
        public virtual ITradingSimpleOrder CreateStopLimitOrder(decimal size, decimal price, decimal stopTriggerPrice) { throw new NotSupportedException(); }
        public virtual ITradingSimpleOrder CreateTrailingStopOrder(decimal size, decimal trailingStopPriceOffset) { throw new NotSupportedException(); }

        public virtual ITradingConditionalOrder CreateIFD(ITradingSimpleOrder first, ITradingSimpleOrder second) { throw new NotSupportedException(); }
        public virtual ITradingConditionalOrder CreateOCO(ITradingSimpleOrder first, ITradingSimpleOrder second) { throw new NotSupportedException(); }
        public virtual ITradingConditionalOrder CreateIFDOCO(ITradingSimpleOrder ifdone, ITradingSimpleOrder ocoFirst, ITradingSimpleOrder ocoSecond) { throw new NotSupportedException(); }
    }

    //===============================================================================

    public interface ITradeOrderTranaction
    {
    }

    public class TradeOrderTransactionStatusChangedEventArgs : EventArgs
    {
        public ITradingSimpleOrder Order { get; private set; }
        public TradeOrderTransactionState Status { get; private set; }
        public TradeOrderTransactionState PrevStatus { get; private set; }
    }

    public class TradeOrderStatusChangedEventArgs : EventArgs
    {
        public ITradingSimpleOrder Order { get; private set; }
        public TradingOrderState Status { get; private set; }
        public TradingOrderState PrevStatus { get; private set; }
    }

}
