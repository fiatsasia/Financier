//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;

namespace Financier.Trading
{
    public class Ticker : ITicker
    {
        public decimal BestBidPrice { get; set; }
        public decimal BestAskPrice { get; set; }
        public decimal MidPrice { get; set; }
        public decimal LastTradedPrice { get; set; }
        public DateTime Time { get; set; }
    }
}
