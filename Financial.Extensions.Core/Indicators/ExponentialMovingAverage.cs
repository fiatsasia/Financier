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
#if INDICATOR_TYPED
        public static IObservable<double> ExponentialMovingAverage(this IObservable<double> source, int period)
        {
            return source.Publish(s => s.Take(period).Average().Concat(s)
            .Scan(
                (last, value) => (value - last) * (2.0d / (period + 1)) + last
            ));
        }

        public static IObservable<decimal> ExponentialMovingAverage(this IObservable<decimal> source, int period)
        {
            return source.Publish(s => s.Take(period).Average().Concat(s)
            .Scan(
                (last, value) => (value - last) * (2.0m / (period + 1)) + last
            ));
        }

        public static IObservable<float> ExponentialMovingAverage(this IObservable<float> source, int period)
        {
            return source.Publish(s => s.Take(period).Average().Concat(s)
            .Scan(
                (last, value) => (value - last) * (2.0f / (period + 1)) + last
            ));
        }
#else
        public static IObservable<TSource> ExponentialMovingAverage<TSource>(this IObservable<TSource> source, int period)
        {
            return source.Publish(s => Calculator.Average(s.Take(period)).Concat(s)
            .Scan(
                (last, value) => Calculator.Add(Calculator.Mul(Calculator.Sub(value, last), Calculator.Cast<TSource>(2.0f / (period + 1))), last)
            ));
        }
#endif
        public static IObservable<(TSource Source, TValue Value)> ExponentialMovingAverage<TSource, TValue>(
            this IObservable<TSource> source,
            int period,
            Func<TSource, TValue> priceGetter
        )
        {
            return source.Select(s => (Source: s, Value: priceGetter(s)))
            .Publish(
                indicator => indicator.Take(period).Buffer(period)
                .Select(
                    indicators => (
                        Source: indicators.Last().Source,
                        Value: Calculator.Average(indicators.Select(ind => ind.Value))
                    )
                )
                .Concat(indicator)
                .Scan(
                    (last, value) =>
                    (
                        Source: value.Source,
                        Value: Calculator.Add
                        (
                            Calculator.Mul
                            (
                                Calculator.Sub
                                (
                                    priceGetter(value.Source),
                                    last.Value
                                ),
                                Calculator.Cast<TValue>
                                (
                                    2.0f / (period + 1)
                                )
                            ),
                            last.Value
                        )
                    )
                )
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
