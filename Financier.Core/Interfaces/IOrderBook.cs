//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System.Collections.Generic;

namespace Financier.Trading
{
    public interface IOrderBook
    {
        decimal BestBidPrice { get; }
        decimal BestBidSize { get; }
        decimal BestAskPrice { get; }
        decimal BestAskSize { get; }
        IReadOnlyList<(decimal Price, decimal Size)> Bids { get; }
        IReadOnlyList<(decimal Price, decimal Size)> Asks { get; }
    }
}
