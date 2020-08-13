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

        decimal OrderPrice { get; }
        decimal OrderSize { get; }

        IEnumerable<IExecution> Executions { get; }
        bool TryExecute(DateTime time, decimal executePrice);
        decimal ExecutedPrice { get; }
        decimal ExecutedSize { get; }

        IReadOnlyList<IOrder> Children { get; }
    }

    public interface IOrderFactory
    {
        // Create simple orders
        IOrder MarketPrice(decimal size);
        IOrder LimitPrice(decimal price, decimal size);
        IOrder StopLoss(decimal stopPrice, decimal size);
        IOrder StopLimit(decimal stopPrice, decimal orderPrice, decimal size);

        // Create structured orders
        IOrder TrailingStop(decimal trailingStopPriceOffset, decimal size);
        IOrder StopAndReverse(decimal stopPrice, decimal size);

        // Create conditional orders
        IOrder IFD(IOrder first, IOrder second);
        IOrder OCO(IOrder first, IOrder second);
        IOrder IFDOCO(IOrder ifdone, IOrder ocoFirst, IOrder ocoSecond);
    }
}
