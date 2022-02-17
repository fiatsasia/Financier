﻿//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Collections.Generic;

namespace Financier.Trading
{
    public interface IOrder
    {
        #region Order request parameters
        public string ProductCode { get; }
        OrderType OrderType { get; }
        decimal? OrderSize { get; }
        decimal? OrderPrice { get; }
        decimal? TriggerPrice { get; }      // stop loss, stop limit
        decimal? StopPrice { get; }         // stop limit
        decimal? TrailingOffset { get; }    // trailing stop, trailing stop limit
        decimal? ProfitPrice { get; }       // Take profit
        IReadOnlyList<IOrder> Children { get; }
        #endregion Order request parameters

        Ulid Id { get; }
        DateTime? OpenTime { get; }
        DateTime? CloseTime { get; }
        DateTime ExpirationDate { get; }
        OrderState Status { get; }

        IReadOnlyList<IOrderExecution> Executions { get; }
        decimal? ExecutedPrice { get; }
        decimal? ExecutedSize { get; }
        IOrder Parent { get; }
        IReadOnlyDictionary<string, object> Metadata { get; }
    }

    public interface IOrder<TOrderRequest> : IOrder where TOrderRequest : IOrderRequest
    {
        TOrderRequest Request { get; }
    }
}
