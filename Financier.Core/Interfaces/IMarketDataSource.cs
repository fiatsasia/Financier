//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
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
        IObservable<IOhlcvv> GetOhlcvvSource(string symbol, TimeSpan period, DateTime start, DateTime end);
    }

    public interface IHistoricalSourceCollection : ICollection<IMarketDataSource>
    {
    }
}
