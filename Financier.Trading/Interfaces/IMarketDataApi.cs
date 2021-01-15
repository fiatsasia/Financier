//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Collections.Generic;

namespace Financier.Trading
{
    public interface IMarketDataApi
    {
        // Realtime APIs
        IObservable<IExecution> GetExecutionSource(string path);
        IObservable<ITicker> GetTickerSource(string path);
        IObservable<IOrderBook> GetOrderBookSource(string path);
        IObservable<TOhlc> GetOhlcUpdateSource<TOhlc>(string path, TimeSpan periods) where TOhlc : IOhlc;

        // Historical APIs
        IEnumerable<TOhlc> GetOhlc<TOhlc>(string path, TimeSpan period, DateTime start, DateTime end) where TOhlc : IOhlc;
    }
}
