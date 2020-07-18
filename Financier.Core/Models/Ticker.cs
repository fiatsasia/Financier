//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public class Ticker<TPrice, TSize> : ITicker<TPrice, TSize>
    {
        public TPrice BidPrice { get; set; }
        public TPrice AskPrice { get; set; }
        public TPrice LastTradedPrice { get; set; }
        public DateTime Time { get; set; }
    }
}
