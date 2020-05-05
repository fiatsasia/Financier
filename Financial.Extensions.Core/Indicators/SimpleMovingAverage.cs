//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Reactive.Linq;

namespace Financial.Extensions
{
    public static partial class IndicatorExtensions
    {
        /// <summary>
        /// Simple moving average (SMA)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="period"></param>
        /// <returns></returns>
#if INDICATOR_TYPED
        public static IObservable<double> SimpleMovingAverage(this IObservable<double> source, int period)
        {
            return source.Buffer(period, 1).Select(e => e.Average());
        }

        public static IObservable<decimal> SimpleMovingAverage(this IObservable<decimal> source, int period)
        {
            return source.Buffer(period, 1).Select(e => e.Average());
        }

        public static IObservable<float> SimpleMovingAverage(this IObservable<float> source, int period)
        {
            return source.Buffer(period, 1).Select(e => e.Average());
        }
#else
        public static IObservable<TSource> SimpleMovingAverage<TSource>(this IObservable<TSource> source, int period)
        {
            return source.Buffer(period, 1).Select(e => e.Average());
        }
#endif
        public static IObservable<(TSource Source, TValue Value)> SimpleMovingAverage<TSource, TValue>(
            this IObservable<TSource> source,
            int period,
            Func<TSource, TValue> priceGetter
        )
        {
            return source.Buffer(period, 1).Where(e => e.Count >= period).Select(e =>
            {
                var current = e.Last();
                return (Source: current, Value: e.Average(priceGetter));
            });
        }
    }
}
