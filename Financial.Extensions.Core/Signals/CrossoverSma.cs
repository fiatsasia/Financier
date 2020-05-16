//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Reactive.Linq;

namespace Financial.Extensions.Trading
{
    public static partial class SignalExtensions
    {
        public static IObservable<ICrossoverSignal<TSource, TPrice>> CrossoverSma<TSource, TPrice>(
            this IObservable<TSource> source,
            int periods,
            Func<TSource, DateTime> timeGetter,
            Func<TSource, TPrice> priceGetter)
        {
            return source
            .SimpleMovingAverage(periods, priceGetter)
            .Select(e =>
            {
                var price = priceGetter(e.Source);
                var sma = e.Value;
                return new CrossoverSignal<TSource, TPrice>
                {
                    Time = timeGetter(e.Source),
                    Signal = Calculator.CompareTo(price, sma),
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
        /// <param name="timeGetter"></param>
        /// <param name="priceGetter"></param>
        /// <returns></returns>
        public static IObservable<ICrossoverSignal<TSource, TPrice>> CrossoverSma<TSource, TPrice>(
            this IObservable<TSource> source,
            int shortPeriods,
            int longPeriods,
            Func<TSource, DateTime> timeGetter,
            Func<TSource, TPrice> priceGetter)
        {
            if (shortPeriods >= longPeriods)
            {
                throw new ArgumentException($"{nameof(shortPeriods)} must be less than {nameof(longPeriods)}");
            }

            return source.Publish(s => s.SimpleMovingAverage(shortPeriods, priceGetter).WithLatestFrom(
                s.SimpleMovingAverage(longPeriods, priceGetter),
                (sp, lp) =>
                new CrossoverSignal<TSource, TPrice>
                {
                    Time = timeGetter(sp.Source),
                    Signal = Calculator.CompareTo(sp.Value, lp.Value),
                    BasePrice = lp.Value,
                    TriggerPrice = sp.Value,
                    Source = sp.Source,
                }
            ));
        }
    }
}
