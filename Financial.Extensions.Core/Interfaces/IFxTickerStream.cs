//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/feedex/
//

using System;

namespace Financial.Extensions
{
    public interface IFxTickerStream
    {
        DateTime Time { get; }
        decimal BestBidPrice { get; }
        decimal BestBidSize { get; }
        decimal BestAskPrice { get; }
        decimal BestAskSize { get; }
    }
}
