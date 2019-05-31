//==============================================================================
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
        /// Simple moving average (SMA)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="period"></param>
        /// <returns></returns>
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
    }
}
