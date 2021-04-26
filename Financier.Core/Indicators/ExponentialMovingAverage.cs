//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Linq;
using System.Reactive.Linq;

namespace Financier
{
    public static partial class IndicatorExtensions
    {
        public static IObservable<double> ExponentialMovingAverage(this IObservable<double> source, int period) =>
            source.Publish(s => s.Take(period).Average().Concat(s).Scan((last, value) => (value - last) * (2.0d / (period + 1)) + last));

        public static IObservable<decimal> ExponentialMovingAverage(this IObservable<decimal> source, int period) =>
            source.Publish(s => s.Take(period).Average().Concat(s).Scan((last, value) => (value - last) * (2.0m / (period + 1)) + last));

        public static IObservable<float> ExponentialMovingAverage(this IObservable<float> source, int period) =>
            source.Publish(s => s.Take(period).Average().Concat(s).Scan((last, value) => (value - last) * (2.0f / (period + 1)) + last));

        public static IObservable<(TSource Source, double Value)> ExponentialMovingAverage<TSource>(this IObservable<TSource> source, int period, Func<TSource, double> selector)
        {
            return source.Select(s => (Source: s, Value: selector(s)))
            .Publish(
                indicator => indicator.Take(period).Buffer(period)
                .Select(indicators => (Source: indicators.Last().Source, Value: indicators.Average(ind => ind.Value)))
                .Concat(indicator)
                .Scan((last, value) => (Source: value.Source, Value: (selector(value.Source) - last.Value) / (2.0d / (period + 1)) + last.Value))
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="period"></param>
        /// <param name="alpha">0.0 < alpha < 1.0</param>
        /// <returns></returns>
        public static IObservable<TSource> ExponentialMovingAverage<TSource>(this IObservable<TSource> source, int period, double alpha)
        {
            throw new NotImplementedException();
        }

    }
}
