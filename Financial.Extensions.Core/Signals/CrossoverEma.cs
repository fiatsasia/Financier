//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Reactive.Linq;

namespace Financial.Extensions
{
    public static partial class SignalExtensions
    {
        public static IObservable<ITradingSignal<TSource, TPrice>> CrossoverEma<TSource, TPrice>(
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
                    Price = price,
                    Source = e.Source,
                };
            });
        }
    }
}
