//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.ObjectModel;

namespace Financier.Trading
{
    public abstract class RealtimeSourceBase<TPrice, TSize> : IRealtimeSource<TPrice, TSize>
    {
        public abstract string Provider { get; }

        public virtual void Connect() => throw new NotSupportedException();

        public virtual IObservable<IExecution> GetExecutionSource(string symbol) => throw new NotSupportedException();
        public virtual IObservable<ITicker<TPrice, TSize>> GetTickerSource(string symbol) => throw new NotSupportedException();
        public virtual IObservable<IOrderBook<TPrice, TSize>> GetOrderBookSource(string symbol) => throw new NotSupportedException();

        // OHLC sources
        public virtual IObservable<IOhlcv<TPrice>> GetOhlcSource(string symbol, TimeSpan period) => GetOhlcvSource(symbol, period);
        public virtual IObservable<IOhlcv<TPrice>> GetOhlcvSource(string symbol, TimeSpan period) => GetOhlcvvSource(symbol, period);
        public virtual IObservable<IOhlcvv<TPrice>> GetOhlcvvSource(string symbol, TimeSpan period) => throw new NotSupportedException();

        // OHLC update sources
        public virtual IObservable<IOhlcv<TPrice>> GetOhlcUpdateSource(string symbol, TimeSpan period) => GetOhlcvUpdateSource(symbol, period);
        public virtual IObservable<IOhlcv<TPrice>> GetOhlcvUpdateSource(string symbol, TimeSpan period) => GetOhlcvvUpdateSource(symbol, period);
        public virtual IObservable<IOhlcv<TPrice>> GetOhlcvvUpdateSource(string symbol, TimeSpan period) => throw new NotSupportedException();
    }

    public class RealtimeSourceCollection : Collection<IMarketDataSource>, IRealtimeSourceCollection
    {
    }

    public class HistoricalSourceCollection : Collection<IMarketDataSource>, IHistoricalSourceCollection
    {
    }
}
