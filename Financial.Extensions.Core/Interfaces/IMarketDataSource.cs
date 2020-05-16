//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financial.Extensions.Trading
{
    public interface IMarketDataSource
    {
        string Provider { get; }
    }

    public interface IRealtimeSource<TPrice, TSize> : IMarketDataSource
    {
        void Connect();

        IObservable<IExecution<TPrice, TSize>> GetExecutionSource(string symbol);
        IObservable<ITicker<TPrice, TSize>> GetTickerSource(string symbol);
        IObservable<IOrderBook<TPrice, TSize>> GetOrderBookSource(string symbol);

        IObservable<IOhlcv<TPrice>> GetOhlcSource(string symbol, TimeSpan period);
        IObservable<IOhlcv<TPrice>> GetOhlcvSource(string symbol, TimeSpan period);
        IObservable<IOhlcvv<TPrice>> GetOhlcvvSource(string symbol, TimeSpan period);

        IObservable<IOhlcv<TPrice>> GetOhlcUpdateSource(string symbol, TimeSpan period);
        IObservable<IOhlcv<TPrice>> GetOhlcvUpdateSource(string symbol, TimeSpan period);
        IObservable<IOhlcv<TPrice>> GetOhlcvvUpdateSource(string symbol, TimeSpan period);
    }

    public interface IRealtimeSourceCollection : ICollection<IMarketDataSource>
    {
    }

    public interface IHistoricalSource<TPrice, TSize> : IMarketDataSource
    {
        IObservable<IOhlcvv<TPrice>> GetOhlcvvSource(string symbol, TimeSpan period, DateTime start, DateTime end);
    }

    public interface IHistoricalSourceCollection : ICollection<IMarketDataSource>
    {
    }
}
