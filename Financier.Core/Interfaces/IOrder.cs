//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financier.Trading
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

        IReadOnlyList<IOrder<TPrice, TSize>> Children { get; }
    }

    public interface IOrderFactory<TPrice, TSize>
    {
        // Create simple orders
        IOrder<TPrice, TSize> CreateMarketPriceOrder(TSize size);
        IOrder<TPrice, TSize> CreateLimitPriceOrder(TPrice price, TSize size);
        IOrder<TPrice, TSize> CreateStopOrder(TPrice stopPrice, TSize size);
        IOrder<TPrice, TSize> CreateStopLimitOrder(TPrice stopPrice, TPrice orderPrice, TSize size);

        // Create structured orders
        IOrder<TPrice, TSize> CreateTrailingStopOrder(TPrice trailingStopPriceOffset, TSize size);
        IOrder<TPrice, TSize> CreateStopAndReverseOrder(TPrice stopPrice, TSize size);

        // Create conditional orders
        IOrder<TPrice, TSize> CreateIFD(IOrder first, IOrder second);
        IOrder<TPrice, TSize> CreateOCO(IOrder first, IOrder second);
        IOrder<TPrice, TSize> CreateIFDOCO(IOrder ifdone, IOrder ocoFirst, IOrder ocoSecond);
    }
}
