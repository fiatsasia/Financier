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
        /// Relative Strength Index (RSI)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static IObservable<double> RelativeStrengthIndex(this IObservable<double> source, int period)
        {
            return source.Buffer(2, 1).Publish(
                s => s.Select(values => (values[0] < values[1]) ? values[1] - values[0] : 0.0)
                    .ModifiedMovingAverage(period)
                    .Zip(s.Select(values => (values[0] > values[1]) ? values[0] - values[1] : 0.0)
                        .ModifiedMovingAverage(period),
                        (mmaUptickSize, mmaDowntickSize) => mmaUptickSize / mmaDowntickSize
                    )
                .Select(rs => (100.0 - 100.0 / (1.0 + rs)))
            );
        }

        public static IObservable<decimal> RelativeStrengthIndex(this IObservable<decimal> source, int period)
        {
            return source.Buffer(2, 1).Publish(
                s => s.Select(values => (values[0] < values[1]) ? values[1] - values[0] : decimal.Zero)
                    .ModifiedMovingAverage(period)
                    .Zip(s.Select(values => (values[0] > values[1]) ? values[0] - values[1] : decimal.Zero)
                        .ModifiedMovingAverage(period),
                        (mmaUptickSize, mmaDowntickSize) => mmaUptickSize / mmaDowntickSize
                    )
                .Select(rs => (100.0m - 100.0m / (decimal.One + rs)))
            );
        }

        public static IObservable<float> RelativeStrengthIndex(this IObservable<float> source, int period)
        {
            return source.Buffer(2, 1).Publish(
                s => s.Select(values => (values[0] < values[1]) ? values[1] - values[0] : 0.0f)
                    .ModifiedMovingAverage(period)
                    .Zip(s.Select(values => (values[0] > values[1]) ? values[0] - values[1] : 0.0f)
                        .ModifiedMovingAverage(period),
                        (mmaUptickSize, mmaDowntickSize) => mmaUptickSize / mmaDowntickSize
                    )
                .Select(rs => (100.0f - 100.0f / (1.0f + rs)))
            );
        }
    }
}
