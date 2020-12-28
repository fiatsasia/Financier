//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;

namespace Financier.Trading
{
    public class OrderRequest : IOrderRequest
    {
        public OrderType OrderType { get; set; }
        public decimal? OrderSize { get; set; }
        public decimal? OrderPrice { get; set; }
        public decimal? TriggerPrice { get; set; }
        public decimal? StopPrice { get; set; }
        public decimal? TrailingOffset { get; set; }
        public decimal? ProfitPrice { get; set; }

        public TimeInForce TimeInForce { get; set; }
        public TimeSpan TimeToExpire { get; set; }

        static readonly IOrderRequest[] _emptyChildren = new IOrderRequest[0];
        public IOrderRequest[] Children { get; set; } = _emptyChildren;
    }
}
