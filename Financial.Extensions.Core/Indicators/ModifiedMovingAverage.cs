﻿//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/feedex/
//

using System;
using System.Linq;
using System.Reactive.Linq;

namespace Financial.Extensions
{
    public static partial class Indicators
    {
        /// <summary>
        /// Modified moving average (MMA)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static IObservable<double> ModifiedMovingAverage(this IObservable<double> source, int period)
        {
            return source.Publish(s => s.Take(period).Average().Concat(s)
            .Scan(
                (last, value) => (last * (period - 1) + value) / period
            ));
        }

        public static IObservable<decimal> ModifiedMovingAverage(this IObservable<decimal> source, int period)
        {
            return source.Publish(s => s.Take(period).Average().Concat(s)
            .Scan(
                (last, value) => (last * (period - 1) + value) / period
            ));
        }

        public static IObservable<float> ModifiedMovingAverage(this IObservable<float> source, int period)
        {
            return source.Publish(s => s.Take(period).Average().Concat(s)
            .Scan(
                (last, value) => (last * (period - 1) + value) / period
            ));
        }
    }
}
