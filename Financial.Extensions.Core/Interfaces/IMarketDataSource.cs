//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financial.Extensions
{
    public interface IMarketDataSource
    {
        string Provider { get; }
    }

    public interface IRealtimeSource<TPrice, TSize> : IMarketDataSource
    {
        IObservable<IOhlcvv<TPrice>> GetOhlcvvSource(string symbol, TimeSpan period);
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

    // Signaling sources
    //
    public interface IHistoricalSignalSource<TSource, TPrice> : IMarketDataSource
    {
        // Read/Write

        // Crossoverとraw signalとの取り扱い
        IObservable<ITradingSignal<TSource, TPrice>> GetSignalSource(string symbol, DateTime start, DateTime end);
    }

    public interface IHistoricalSignalSourceCollection : ICollection<IMarketDataSource>
    {
    }

    public interface IRealtimeSignalSource : IMarketDataSource
    {
    }
}
