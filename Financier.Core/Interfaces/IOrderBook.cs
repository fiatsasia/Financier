//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier;

public interface IOrderlet
{
    decimal Price { get; }
    decimal Size { get; }
}

public interface IOrderBook
{
    decimal BestBidPrice { get; }
    decimal BestBidSize { get; }
    decimal BestAskPrice { get; }
    decimal BestAskSize { get; }
    IReadOnlyList<IOrderlet> Bids { get; }
    IReadOnlyList<IOrderlet> Asks { get; }
}
