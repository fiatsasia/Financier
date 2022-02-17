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

namespace Financier
{
    public interface IMarketDataSource
    {
        string Provider { get; }
    }

    public interface IRealtimeSource : IMarketDataSource
    {
        Task OpenAsync();

        IObservable<IExecution> GetExecutionSource(string symbol);
        IObservable<ITicker> GetTickerSource(string symbol);
        IObservable<IOrderBook> GetOrderBookSource(string symbol);
        IObservable<TOhlc> GetOhlcSource<TOhlc>(string symbol, TimeSpan frameSpan) where TOhlc : IOhlc;
        IObservable<TOhlc> GetOhlcUpdateSource<TOhlc>(string symbol, TimeSpan frameSpan) where TOhlc : IOhlc;
    }

    public interface IRealtimeSourceCollection : ICollection<IRealtimeSource>
    {
    }

    public interface IHistoricalSource : IMarketDataSource
    {
        IEnumerable<IExecution> GetExecutionSource(string symbol, DateTime start, TimeSpan span);
        IEnumerable<TOhlc> GetOhlcSource<TOhlc>(string symbol, TimeSpan frameSpan, DateTime start, TimeSpan span) where TOhlc : IOhlc;
    }

    public interface IHistoricalSourceCollection : ICollection<IHistoricalSource>
    {
    }
}
