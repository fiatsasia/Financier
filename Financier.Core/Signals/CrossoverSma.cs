//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
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
        public static IObservable<ICrossoverSignal<TSource, double>> CrossoverSma<TSource>(this IObservable<TSource> source,
            int periods,
            Func<TSource, DateTime> timeSelector,
            Func<TSource, double> valueSelector
        )
        {
            return source
            .SimpleMovingAverage(periods, valueSelector)
            .Select(e =>
            {
                var price = valueSelector(e.Source);
                var sma = e.Value;
                return new CrossoverSignal<TSource, double>
                {
                    Time = timeSelector(e.Source),
                    Signal = price.CompareTo(sma),
                    BasePrice = sma,
                    TriggerPrice = price,
                    Source = e.Source,
                };
            });
        }

        /// <summary>
        /// Generate crossover SMA(short periods) VS SMA(long periods) signal.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TPrice"></typeparam>
        /// <param name="source"></param>
        /// <param name="shortPeriods"></param>
        /// <param name="longPeriods"></param>
        /// <param name="timeSelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static IObservable<ICrossoverSignal<TSource, double>> CrossoverSma<TSource>(this IObservable<TSource> source,
            int shortPeriods,
            int longPeriods,
            Func<TSource, DateTime> timeSelector,
            Func<TSource, double> valueSelector)
        {
            if (shortPeriods >= longPeriods)
            {
                throw new ArgumentException($"{nameof(shortPeriods)} must be less than {nameof(longPeriods)}");
            }

            return source.Publish(s => s.SimpleMovingAverage(shortPeriods, valueSelector).WithLatestFrom(
                s.SimpleMovingAverage(longPeriods, valueSelector),
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
