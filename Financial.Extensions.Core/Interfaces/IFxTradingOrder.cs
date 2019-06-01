//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/feedex/
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financial.Extensions
{
    public enum FxTradingOrderType
    {
        Unknown,

        Limit,
        Market,
        Stop,
        StopLimit,
        TrailingStop,
        Streming,
    }

    public enum FxTradeParentOrderType
    {
        IFD,
        OCO,
    }

    public interface IFxTradingAccount
    {
        void Login(string key, string secret);
        IFxTradingMarket GetMarket(string marketSymbol);
    }

    public interface IFxTradingMarket
    {
        string MarketSymbol { get; }

        FxTradingOrderFactoryBase GetTradeOrderFactory();

        Task PlaceOrder(IFxTradingOrder order);

        IEnumerable<IFxTradingOrder> ListOrders();
        IEnumerable<IFxTradingPosition> ListPositions();

        event Action<IFxTradingOrder> OrderChanged;
        event Action<IFxTradingPosition> PositionChanged;

        decimal BestBidPrice { get; }
        decimal BestBidSize { get; }
        decimal BestAskPrice { get; }
        decimal BestAskSize { get; }
    }

    public enum FxTradingOrderState
    {
        New,
        PartiallyFilled,
        Filled,
        Canceled,
        Rejected, // 注文を送信して、サイズ不正などで失敗した場合、IFDなどで後続注文が失敗した場合など
        Expired,
    }

    public interface IFxTradingOrder
    {
        Guid OrderId { get; }
        DateTime OpenTime { get; }
        DateTime CloseTime { get; }
        FxTradingOrderState Status { get; }
        object Tag { get; set; }

        event Action<IFxTradingSimpleOrder> OrderChanged;

        void CancelOrder();
    }

    public interface IFxTradingSimpleOrder : IFxTradingOrder
    {
        FxTradingOrderType OrderType { get; }
        decimal OrderPrice { get; }
        decimal OrderSize { get; }
        decimal StopTriggerPrice { get; }
        decimal TrailingStopOffset { get; }
    }

    public interface IFxTradingParentOrder : IFxTradingOrder
    {
        FxTradeParentOrderType OrderType { get; }
        IReadOnlyList<IFxTradingOrder> ChildOrders { get; }
        IFxTradingSimpleOrder CurrentOrder { get; }
    }

    public abstract class FxTradingOrderFactoryBase
    {
        public virtual IFxTradingSimpleOrder CreateMarketPriceOrder(FxTradeSide side, decimal size) { throw new NotSupportedException(); }
        public virtual IFxTradingSimpleOrder CreateLimitPriceOrder(FxTradeSide side, decimal price, decimal size) { throw new NotSupportedException(); }
        public virtual IFxTradingSimpleOrder CreateStopOrder(decimal size, decimal stopTriggerPrice) { throw new NotSupportedException(); }
        public virtual IFxTradingSimpleOrder CreateStopLimitOrder(decimal size, decimal price, decimal stopTriggerPrice) { throw new NotSupportedException(); }
        public virtual IFxTradingSimpleOrder CreateTrailingStopOrder(decimal size, decimal trailingStopPriceOffset) { throw new NotSupportedException(); }

        public virtual IFxTradingParentOrder CreateIFD(IFxTradingSimpleOrder first, IFxTradingSimpleOrder second) { throw new NotSupportedException(); }
        public virtual IFxTradingParentOrder CreateOCO(IFxTradingSimpleOrder first, IFxTradingSimpleOrder second) { throw new NotSupportedException(); }
        public virtual IFxTradingParentOrder CreateIFDOCO(IFxTradingSimpleOrder ifdone, IFxTradingSimpleOrder ocoFirst, IFxTradingSimpleOrder ocoSecond) { throw new NotSupportedException(); }
    }

    public enum FxTradePositionState
    {
        Active,
        Closed,
    }

    public interface IFxTradingPosition
    {
        Guid PositionId { get; }
        FxTradePositionState Status { get; }
        event Action<IFxTradingPosition> PositionChanged;
    }

    public enum FxTradeTimeInForce
    {
        GoodTilCanceled,
        FillOrKill,
    }

    //===============================================================================

    public enum FxTradeOrderTransactionState
    {
        Created,
        Accepted,
        Confirmed,
        Failed,
    }

    public interface IFxTradeOrderTranaction
    {
    }

    public class FxTradeOrderTransactionStatusChangedEventArgs : EventArgs
    {
        public IFxTradingSimpleOrder Order { get; private set; }
        public FxTradeOrderTransactionState Status { get; private set; }
        public FxTradeOrderTransactionState PrevStatus { get; private set; }
    }

    public class FxTradeOrderStatusChangedEventArgs : EventArgs
    {
        public IFxTradingSimpleOrder Order { get; private set; }
        public FxTradingOrderState Status { get; private set; }
        public FxTradingOrderState PrevStatus { get; private set; }
    }

}
