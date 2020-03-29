//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Reactive.Linq;

namespace Financial.Extensions.Indicators
{
    public static partial class IndicatorExtensions
    {
        /// <summary>
        /// Simple moving average (SMA)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static IObservable<float> SimpleMovingAverage(this IObservable<float> source, int period)
        {
            return source.Buffer(period, 1).Select(e => e.Average());
        }

        public static IObservable<double> SimpleMovingAverage(this IObservable<double> source, int period)
        {
            return source.Buffer(period, 1).Select(e => e.Average());
        }

        public static IObservable<decimal> SimpleMovingAverage(this IObservable<decimal> source, int period)
        {
            return source.Buffer(period, 1).Select(e => e.Average());
        }

        public static IObservable<(DateTime Time, float Value, float Result)> SimpleMovingAverage(this IObservable<(DateTime Time, float Value)> source, int period)
        {
            return source.Buffer(period, 1).Where(e => e.Count >= period).Select(e =>
            {
                var current = e.Last();
                return (current.Time, current.Value, e.Average(f => f.Value));
            });
        }

        public static IObservable<(DateTime Time, double Value, double Result)> SimpleMovingAverage(this IObservable<(DateTime Time, double Value)> source, int period)
        {
            return source.Buffer(period, 1).Where(e => e.Count >= period).Select(e =>
            {
                var current = e.Last();
                return (current.Time, current.Value, e.Average(f => f.Value));
            });
        }

        public static IObservable<(DateTime Time, decimal Value, decimal Result)> SimpleMovingAverage(this IObservable<(DateTime Time, decimal Value)> source, int period)
        {
            return source.Buffer(period, 1).Where(e => e.Count >= period).Select(e =>
            {
                var current = e.Last();
                return (current.Time, current.Value, e.Average(f => f.Value));
            });
        }
    }
}
