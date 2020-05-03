//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions.Trading
{
    public class MarketIndicator<TSource, TPrice> : IMarketIndicator<TSource, TPrice>
    {
        public TSource Source { get; set; }
        public TPrice Value { get; set; }
    }
}
