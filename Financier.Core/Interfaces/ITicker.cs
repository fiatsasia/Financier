//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;

namespace Financier
{
    public interface ITicker
    {
        DateTime Time { get; }
        decimal BestBidPrice { get; }
        decimal BestAskPrice { get; }
        decimal LastTradedPrice { get; }
    }
}
