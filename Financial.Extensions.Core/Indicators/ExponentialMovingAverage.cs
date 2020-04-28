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
        // Original formula is below.
        // (last, value) => value * 2.0 / (period + 1) + last * (1.0 - 2.0 / (period + 1))
        // i.e. https://en.wikipedia.org/wiki/Moving_average
        // And optimized formula is derived by below steps
        // alpha = 2.0m / (period + 1)
        // ema = alpha * value + (1.0 - alpha) * last
        // ema = alpha * value + last - alpha * last
        // ema = alpha * (value - last) + last
        // ema = 2.0m / (period + 1) * (value - last) + last
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
        public static IObservable<IMarketIndicator<TSource, TValue>> ExponentialMovingAverage<TSource, TValue>(
            this IObservable<TSource> source,
            int period,
            Func<TSource, TValue> priceGetter
        )
        {
            return source.Select(s => new MarketIndicator<TSource, TValue> { Source = s, Value = priceGetter(s) })
            .Publish(
                indicator => indicator.Take(period).Buffer(period)
                    .Select(
                        indicators => new MarketIndicator<TSource, TValue>
                        {
                            Source = indicators.Last().Source,
                            Value = Calculator.Average(indicators.Select(ind => ind.Value))
                        }
                    )
                .Concat(indicator)
                .Scan(
                    (last, value) =>
                    {
                        return new MarketIndicator<TSource, TValue>
                        {
                            Source = value.Source,
                            Value = Calculator.Add(Calculator.Mul(Calculator.Sub(value.Value, last.Value), Calculator.Cast<TValue>(2.0f / (period + 1))), last.Value)
                        };
                    }
                )
            );
        }
    }
}
