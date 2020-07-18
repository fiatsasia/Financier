//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public interface ITicker
    {
        DateTime Time { get; }
    }

    public interface ITicker<TPrice, TSize> : ITicker
    {
        TPrice BidPrice { get; }
        TPrice AskPrice { get; }
        TPrice LastTradedPrice { get; }
    }
}
