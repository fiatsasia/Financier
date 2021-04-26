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

namespace Financier.Signals
{
    public static partial class SignalExtensions
    {
        public static IObservable<ICrossoverSignal<TSource, double>> CrossoverEma<TSource>(
            this IObservable<TSource> source,
            int periods,
            Func<TSource, DateTime> timeSelector,
            Func<TSource, double> valueSelector)
        {
            return source
            .ExponentialMovingAverage(periods, valueSelector)
            .Select(e =>
            {
                var price = valueSelector(e.Source);
                var ema = e.Value;
                return new CrossoverSignal<TSource, double>
                {
                    Time = timeSelector(e.Source),
                    Signal = price.CompareTo(ema),
                    BasePrice = ema,
                    TriggerPrice = price,
                    Source = e.Source,
                };
            });
        }

        public static IObservable<ICrossoverSignal<TSource, double>> CrossoverEma<TSource>(
            this IObservable<TSource> source,
            int shortPeriods,
            int longPeriods,
            Func<TSource, DateTime> timeSelector,
            Func<TSource, double> valueSelector)
        {
            if (shortPeriods >= longPeriods)
            {
                throw new ArgumentException($"{nameof(shortPeriods)} must be less than {nameof(longPeriods)}");
            }

            return source.Publish(s => s.ExponentialMovingAverage(shortPeriods, valueSelector).WithLatestFrom(
                s.ExponentialMovingAverage(longPeriods, valueSelector),
                (sp, lp) =>
                new CrossoverSignal<TSource, double>
                {
                    Time = timeSelector(sp.Source),
                    Signal = sp.Value.CompareTo(lp.Value),
                    BasePrice = lp.Value,
                    TriggerPrice = sp.Value,
                    Source = sp.Source,
                }
            ));
        }
    }
}
