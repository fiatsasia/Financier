//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Reactive.Linq;

namespace Financial.Extensions
{
    public static partial class Indicators
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
    }
}
