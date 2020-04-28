﻿//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public interface ITickerStream
    {
        DateTime Time { get; }
    }

    public interface ITickerStream<TPrice, TSize> : ITickerStream
    {
        TPrice BestBidPrice { get; }
        TSize BestBidSize { get; }
        TPrice BestAskPrice { get; }
        TSize BestAskSize { get; }
    }
}
