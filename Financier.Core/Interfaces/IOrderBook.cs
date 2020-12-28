//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
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
