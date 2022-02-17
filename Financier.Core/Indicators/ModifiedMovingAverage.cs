﻿//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
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
