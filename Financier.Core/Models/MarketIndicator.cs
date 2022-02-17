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
    public class MarketIndicator<TSource, TPrice> : IMarketIndicator<TSource, TPrice>
    {
        public TSource Source { get; set; }
        public TPrice Value { get; set; }
    }
}
