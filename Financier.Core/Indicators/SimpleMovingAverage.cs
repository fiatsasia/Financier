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
using System.Reactive.Disposables;

namespace Financier
{
    public static partial class IndicatorExtensions
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

        public static IObservable<(TSource Source, double Value)> SimpleMovingAverage<TSource>(this IObservable<TSource> source,
            int period,
            Func<TSource, double> selector,
            bool includeInitial = false
        )
        {
            if (includeInitial)
            {
                return Observable.Create<(TSource Source, double Value)>(observer =>
                {
                    var initial = default(double[]);
                    var index = 0;
                    var disposable = source.Subscribe(current =>
                    {
                        if (initial == default)
                        {
                            initial = Enumerable.Repeat(selector(current), period).ToArray();
                        }
                        else
                        {
                            initial[index] = selector(current);
                        }
                        index = (index + 1) % period; // store as ring-buffer
                        observer.OnNext((Source: current, Value: initial.Average()));
                    });
                    return Disposable.Create(disposable.Dispose);
                });
            }
            else
            {
                return source.Buffer(period, 1).Where(e => e.Count >= period).Select(e =>
                {
                    var current = e.Last();
                    return (Source: current, Value: e.Average(f => selector(f)));
                });
            }
        }
    }
}
