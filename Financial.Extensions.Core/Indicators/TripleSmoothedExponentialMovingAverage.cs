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
        /// Triple exponentially smoothed moving average (TRIX)
        /// <see href="https://en.wikipedia.org/wiki/Trix_(technical_analysis)" />
        /// </summary>
        /// <param name="source"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static IObservable<double> TripleSmoothedExponentialMovingAverage(this IObservable<double> source, int period)
        {
            return source.ExponentialMovingAverage(period)
            .ExponentialMovingAverage(period)
            .ExponentialMovingAverage(period)
            .Buffer(2, 1)
            .Select(values =>
            {
                return (values[1] - values[0]) / values[0];
            });
        }

        public static IObservable<decimal> TripleSmoothedExponentialMovingAverage(this IObservable<decimal> source, int period)
        {
            return source.ExponentialMovingAverage(period)
            .ExponentialMovingAverage(period)
            .ExponentialMovingAverage(period)
            .Buffer(2, 1)
            .Select(values =>
            {
                return (values[1] - values[0]) / values[0];
            });
        }

        public static IObservable<float> TripleSmoothedExponentialMovingAverage(this IObservable<float> source, int period)
        {
            return source.ExponentialMovingAverage(period)
            .ExponentialMovingAverage(period)
            .ExponentialMovingAverage(period)
            .Buffer(2, 1)
            .Select(values =>
            {
                return (values[1] - values[0]) / values[0];
            });
        }
    }
}
