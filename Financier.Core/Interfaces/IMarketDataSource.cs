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
    public interface IMarketDataSource
    {
        string Provider { get; }
    }

    public interface IRealtimeSource : IMarketDataSource
    {
        void Connect();

        IObservable<IExecution> GetExecutionSource(string symbol);
        IObservable<ITicker> GetTickerSource(string symbol);
        IObservable<IOrderBook> GetOrderBookSource(string symbol);
        IObservable<TOhlc> GetOhlcSource<TOhlc>(string symbol, TimeSpan period) where TOhlc : IOhlc;
        IObservable<TOhlc> GetOhlcUpdateSource<TOhlc>(string symbol, TimeSpan period) where TOhlc : IOhlc;
    }

    public interface IRealtimeSourceCollection : ICollection<IRealtimeSource>
    {
    }

    public interface IHistoricalSource : IMarketDataSource
    {
        IEnumerable<TOhlc> GetOhlcSource<TOhlc>(string symbol, TimeSpan period, DateTime start, DateTime end) where TOhlc : IOhlc;
    }

    public interface IHistoricalSourceCollection : ICollection<IMarketDataSource>
    {
    }
}
