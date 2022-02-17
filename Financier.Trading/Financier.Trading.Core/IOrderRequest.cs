//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;

namespace Financier.Trading
{
    public interface IOrderRequest
    {
        Ulid Id { get; set; }
        string ProductCode { get; set; }
        OrderType OrderType { get; set; }
        decimal? OrderSize { get; set; }
        decimal? OrderPrice { get; set; }
        decimal? TriggerPrice { get; set; }      // stop loss, stop limit
        decimal? StopPrice { get; set; }         // stop limit
        decimal? TrailingOffset { get; set; }    // trailing stop, trailing stop limit
        decimal? ProfitPrice { get; set; }       // Take profit

        TimeInForce? TimeInForce { get; set; }
        TimeSpan? TimeToExpire { get; set; }
    }

    public interface IOrderRequest<TChild> : IOrderRequest where TChild : IOrderRequest
    {
        TChild[] Children { get; set; }
    }
}
