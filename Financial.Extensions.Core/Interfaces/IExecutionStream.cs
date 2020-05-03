//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public interface IExecutionStream
    {
        DateTime Time { get; }
        long Serial { get; }
    }

    public interface IExecutionStream<TPrice, TSize> : IExecutionStream
    {
        TPrice Price { get; }
        TSize Size { get; }       // +buy -sell
    }
}
