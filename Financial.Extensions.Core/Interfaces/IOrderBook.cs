//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System.Collections.Generic;

namespace Financial.Extensions.Trading
{
    public interface IOrderBook<TPrice, TSize>
    {
        TPrice BestBidPrice { get; }
        TSize BestBidSize { get; }
        TPrice BestAskPrice { get; }
        TSize BestAskSize { get; }
        IReadOnlyList<(TPrice Price, TSize Size)> Bids { get; }
        IReadOnlyList<(TPrice Price, TSize Size)> Asks { get; }
    }
}
