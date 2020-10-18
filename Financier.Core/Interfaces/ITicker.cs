//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public interface ITicker
    {
        DateTime Time { get; }
        decimal BestBidPrice { get; }
        decimal BestAskPrice { get; }
        decimal MidPrice { get; }
        decimal LastTradedPrice { get; }
    }
}
