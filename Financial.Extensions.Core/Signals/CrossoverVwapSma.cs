//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Reactive.Linq;
using Financial.Extensions.Rx;
using Financial.Extensions.Indicators;

namespace Financial.Extensions.Signals
{
    public static partial class SignalExtensions
    {
        public static IObservable<(DateTime Time, double Vwap, double Sma, int Signal)> CrossoverVwapSma<T>(this IObservable<IOhlcvv<T>> source, int periods, int intervalMinutes = 1) where T : IComparable
        {
            if (intervalMinutes > 1)
            {
                source = source
                    .BufferByPeriod(e => e.Start, TimeSpan.FromMinutes(intervalMinutes)).Select(e => new OhlcvvModel<T>(TimeSpan.FromMinutes(intervalMinutes), e));
            }

            return source
                .ComplementPeriods(
                    e => e.Start,
                    (d, e) => new OhlcvvModel<T>(TimeSpan.FromMinutes(intervalMinutes), d, e.Close, e.Close, e.Close, e.Close, 0d, e.VWAP),
                    intervalMinutes
                )
                .Select(e => (e.Start, e.VWAP))
                .SimpleMovingAverage(periods)
                .Select(e =>
                {
                    var signal = 0;
                    if (e.Value > e.Result)
                    {
                        signal = 1;
                    }
                    else if (e.Value < e.Result)
                    {
                        signal = -1;
                    }
                    return (e.Time, e.Value, e.Result, signal);
                })
                .Where(e => e.signal != 0).Buffer(2, 1).Where(e => e.Count >= 2 && e[0].signal != e[1].signal).Select(e => e[1]);
        }
    }
}
