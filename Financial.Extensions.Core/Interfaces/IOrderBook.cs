//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financial.Extensions
{
    public interface IOrderBook<TPrice, TSize>
    {
        TPrice BestBidPrice { get; }
        TSize BestBidSize { get; }
        TPrice BestAskPrice { get; }
        TSize BestAskSize { get; }

        double MidPrice { get; }
        double TotalBidDepth { get; }
        double TotalAskDepth { get; }
    }
}
