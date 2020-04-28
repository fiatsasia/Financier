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
        TradingOrderType OrderType { get; }
        DateTime OpenTime { get; }
        DateTime CloseTime { get; }
        TradingOrderState Status { get; }

        bool IsSettlmentOrder { get; }
        bool HasChildOrder { get; }

        void Open(DateTime time);
    }

    public interface ITradingOrder<TPrice, TSize> : ITradingOrder
    {
        ITradingPosition<TPrice, TSize> Position { get; }
        TPrice OrderPrice { get; }
        TSize OrderSize { get; }
        TPrice ExecutedPrice { get; }
        TSize ExecutedSize { get; }

        ITradingOrder<TPrice, TSize> Parent { get; }
        IReadOnlyList<ITradingOrder<TPrice, TSize>> Children { get; }

        bool CanExecute(TPrice price);
        void Execute(DateTime time, TPrice executePrice);
        void ExecutePartial(DateTime time, TPrice executePrice, TSize executeSize);
    }

    public interface ITradingConditionalOrder : ITradingOrder
    {
        IReadOnlyList<ITradingOrder> ChildOrders { get; }
        ITradingOrder CurrentOrder { get; }
    }

    public interface ITradingOrderFactory<TPrice, TSize>
    {
        // Create simple orders
        ITradingOrder<TPrice, TSize> CreateMarketPriceOrder(TSize size);
        ITradingOrder<TPrice, TSize> CreateLimitPriceOrder(TPrice price, TSize size);
        ITradingOrder<TPrice, TSize> CreateStopOrder(TSize size, TPrice stopTriggerPrice);
        ITradingOrder<TPrice, TSize> CreateStopLimitOrder(TSize size, TPrice price, TPrice stopTriggerPrice);
        ITradingOrder<TPrice, TSize> CreateTrailingStopOrder(TSize size, TPrice trailingStopPriceOffset);

        // Create conditional orders
        ITradingOrder<TPrice, TSize> CreateIFD(ITradingOrder first, ITradingOrder second);
        ITradingOrder<TPrice, TSize> CreateOCO(ITradingOrder first, ITradingOrder second);
        ITradingOrder<TPrice, TSize> CreateIFDOCO(ITradingOrder ifdone, ITradingOrder ocoFirst, ITradingOrder ocoSecond);

        // Create settlement orders
        ITradingOrder<TPrice, TSize> CreateSettlementMarketPriceOrder(ITradingPosition<TPrice, TSize> position);
        ITradingOrder<TPrice, TSize> CreateSettlementLimitPriceOrder(ITradingPosition<TPrice, TSize> position, TPrice price);
        ITradingOrder<TPrice, TSize> CreateStopAndReverseOrder(ITradingPosition<TPrice, TSize> position);
    }
}
