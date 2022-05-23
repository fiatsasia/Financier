using System;
using System.Collections.Generic;
using System.Text;

namespace Financier.Trading
{
    public abstract class MarketDataSourcesBase : IDisposable
    {
        public virtual decimal CurrentPrice { get; }

#if false
        // Realtime APIs
        IObservable<IExecution> GetExecutionSource(string path);
        IObservable<ITicker> GetTickerSource(string path);
        IObservable<IOrderBook> GetOrderBookSource(string path);
        IObservable<TOhlc> GetOhlcUpdateSource<TOhlc>(string path, TimeSpan frameSpan) where TOhlc : IOhlc;

        // Historical APIs
        Task<IEnumerable<TOhlc>> GetOhlc<TOhlc>(string path, TimeSpan frameSpan, DateTime start, TimeSpan span) where TOhlc : IOhlc;
#endif

        public virtual void Dispose()
        {
        }

        public virtual IObservable<IExecution> GetExecutionSource() => throw new NotSupportedException();
        public virtual IObservable<IExecution> GetExecutionSource(DateTime start, TimeSpan span) => throw new NotSupportedException();

        public virtual IObservable<ITicker> GetTickerSource() => throw new NotSupportedException();

        public virtual IObservable<IOrderBook> GetOrderBookSource() => throw new NotSupportedException();

        public virtual IObservable<TOhlc> GetOhlcSource<TOhlc>(TimeSpan frameSpan) where TOhlc : IOhlc => throw new NotSupportedException();
        public virtual IObservable<TOhlc> GetOhlcSource<TOhlc>(TimeSpan frameSpan, DateTime start, TimeSpan span) where TOhlc : IOhlc => throw new NotSupportedException();

        public virtual IObservable<TOhlc> GetOhlcUpdateSource<TOhlc>(TimeSpan frameSpan) where TOhlc : IOhlc => throw new NotSupportedException();
    }
}
