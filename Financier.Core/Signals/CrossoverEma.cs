//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Reactive.Linq;

namespace Financier.Trading
{
    public static partial class SignalExtensions
    {
        public static IObservable<ICrossoverSignal<TSource, TPrice>> CrossoverEma<TSource, TPrice>(
            this IObservable<TSource> source,
            int periods,
            Func<TSource, DateTime> timeGetter,
            Func<TSource, TPrice> priceGetter)
        {
            return source
            .ExponentialMovingAverage(periods, priceGetter)
            .Select(e =>
            {
                var price = priceGetter(e.Source);
                var ema = e.Value;
                return new CrossoverSignal<TSource, TPrice>
                {
                    Time = timeGetter(e.Source),
                    Signal = Calculator.CompareTo(price, ema),
                    BasePrice = ema,
                    TriggerPrice = price,
                    Source = e.Source,
                };
            });
        }

        public static IObservable<ICrossoverSignal<TSource, TPrice>> CrossoverEma<TSource, TPrice>(
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

            return source.Publish(s => s.ExponentialMovingAverage(shortPeriods, priceGetter).WithLatestFrom(
                s.ExponentialMovingAverage(longPeriods, priceGetter),
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
