//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public class Ticker : ITicker
    {
        public decimal BestBidPrice { get; set; }
        public decimal BestAskPrice { get; set; }
        public decimal LastTradedPrice { get; set; }
        public DateTime Time { get; set; }
    }
}
