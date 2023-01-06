//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier;

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
