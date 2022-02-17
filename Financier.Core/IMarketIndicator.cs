//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;

namespace Financier.Trading
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
