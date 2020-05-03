//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions.Trading
{
    public interface IMarketIndicator
    {
    }

    public interface IMarketIndicator<TSource, TPrice>
    {
        TSource Source { get; set; }
        TPrice Value { get; set; }
    }
}
