//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/feedex/
//

using System;

namespace Financial.Extensions
{
    public interface IFxTradeStream
    {
        DateTime Time { get; }
        decimal Price { get; }
        decimal Size { get; }       // +buy -sell
    }
}
