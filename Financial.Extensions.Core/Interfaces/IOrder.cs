//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financial.Extensions.Trading
{
    public interface IOrder
    {
        OrderType OrderType { get; }
        DateTime OpenTime { get; }
        DateTime CloseTime { get; }
        OrderState Status { get; }

        void Open(DateTime time);
        bool IsClosed { get; }
    }

    public interface IOrder<TPrice, TSize> : IOrder
    {
        TPrice OrderPrice { get; }
        TSize OrderSize { get; }

        IEnumerable<IExecution<TPrice, TSize>> Executions { get; }
        bool TryExecute(DateTime time, TPrice executePrice);
        TPrice ExecutedPrice { get; }
        TSize ExecutedSize { get; }
    }

    public interface IConditionalOrder : IOrder
    {
        IReadOnlyList<IOrder> ChildOrders { get; }
        IOrder CurrentOrder { get; }
    }

    public interface IOrderFactory<TPrice, TSize>
    {
        // Create simple orders
        IOrder<TPrice, TSize> CreateMarketPriceOrder(TSize size);
        IOrder<TPrice, TSize> CreateLimitPriceOrder(TPrice price, TSize size);
        IOrder<TPrice, TSize> CreateStopOrder(TSize size, TPrice stopTriggerPrice);
        IOrder<TPrice, TSize> CreateStopLimitOrder(TSize size, TPrice price, TPrice stopTriggerPrice);
        IOrder<TPrice, TSize> CreateTrailingStopOrder(TSize size, TPrice trailingStopPriceOffset);

        // Create conditional orders
        IOrder<TPrice, TSize> CreateIFD(IOrder first, IOrder second);
        IOrder<TPrice, TSize> CreateOCO(IOrder first, IOrder second);
        IOrder<TPrice, TSize> CreateIFDOCO(IOrder ifdone, IOrder ocoFirst, IOrder ocoSecond);
    }
}
