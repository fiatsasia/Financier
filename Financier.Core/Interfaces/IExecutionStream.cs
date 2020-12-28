//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;

namespace Financier
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
