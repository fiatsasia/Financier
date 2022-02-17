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

namespace Financier
{
    public static partial class IndicatorExtensions
    {
        public static IObservable<(TSource Source, double Value)> HistoricalVolatility<TSource>(this IObservable<TSource> source, int period, Func<TSource, double> selector)
        {
            return source
                .Buffer(2, 1).Where(e => e.Count >= 2).Select(e => (Source: e[1], Value: Math.Log(selector(e[1]) / selector(e[0]))))
                .Buffer(period, 1).Where(e => e.Count >= period).Select(e =>
                {
                    var ave = e.Average(e => e.Value);
                    var sum = e.Sum(e => Math.Pow(e.Value - ave, 2));
                    return (Source: e.Last().Source, Value: Math.Sqrt(sum / (period - 1)));
                });
        }
    }
}
