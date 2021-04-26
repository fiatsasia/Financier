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
        public static IObservable<IList<TSource>> Buffer<TSource>(this IObservable<TSource> source, Func<TSource, DateTime> selector, TimeSpan frameSpan)
        {
            var duration = new Subject<Unit>();
            return source
            .Scan((prev, current) =>
            {
                if (selector(prev).Round(frameSpan) != selector(current).Round(frameSpan))
                {
                    duration.OnNext(Unit.Default);
                }
                return current;
            })
            .Buffer(() => duration);
        }
    }
}
