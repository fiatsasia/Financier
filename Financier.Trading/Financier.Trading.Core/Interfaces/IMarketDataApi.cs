//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financier.Trading
{
    public interface IMarketDataSources
    {
        // Realtime APIs
        IObservable<IExecution> GetExecutionSource(string path);
        IObservable<ITicker> GetTickerSource(string path);
        IObservable<IOrderBook> GetOrderBookSource(string path);
        IObservable<TOhlc> GetOhlcUpdateSource<TOhlc>(string path, TimeSpan frameSpan) where TOhlc : IOhlc;

        // Historical APIs
        Task<IEnumerable<TOhlc>> GetOhlc<TOhlc>(string path, TimeSpan frameSpan, DateTime start, TimeSpan span) where TOhlc : IOhlc;
    }
}
