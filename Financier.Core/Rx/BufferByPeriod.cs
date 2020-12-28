//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Financier
{
    public static partial class RxExtensions
    {
        // ** If market data feed from source is infrequent than period, buffer feed will be delayed.

        // With getter method
        public static IObservable<IList<TSource>> BufferByPeriod<TSource>(this IObservable<TSource> source, Func<TSource, DateTime> timeGetter, TimeSpan period)
        {
            var duration = new Subject<Unit>();
            return source
            .Scan((prev, current) =>
            {
                if (timeGetter(prev).Round(period) != timeGetter(current).Round(period))
                {
                    duration.OnNext(Unit.Default);
                }
                return current;
            })
            .Buffer(() => duration);
        }

        // Common trade class
        public static IObservable<IList<TSource>> BufferByPeriod<TSource>(this IObservable<TSource> source, TimeSpan period) where TSource : IExecutionStream
        {
            var duration = new Subject<Unit>();
            return source
            .Scan((prev, current) =>
            {
                if (prev.Time.Round(period) != current.Time.Round(period))
                {
                    duration.OnNext(Unit.Default);
                }
                return current;
            })
            .Buffer(() => duration);
        }
    }
}
