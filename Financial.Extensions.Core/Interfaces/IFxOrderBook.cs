//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financial.Extensions
{
    public interface IFxOrderBook
    {
        decimal BestBidPrice { get; }
        decimal BestBidSize { get; }
        decimal BestAskPrice { get; }
        decimal BestAskSize { get; }

        decimal MidPrice { get; }
        decimal TotalBidDepth { get; }
        decimal TotalAskDepth { get; }
    }
}
