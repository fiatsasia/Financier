//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Financier
{
    public static partial class IndicatorExtensions
    {
        // With getter method
        public static IObservable<double> VWAP<TSource>(this IObservable<IList<TSource>> source, Func<TSource, (double Price, double Size)> priceTimeGetter)
        {
            return source.Select(ticks =>
            {
                double volume = 0.0;
                double amount = 0.0;
                foreach (var tick in ticks)
                {
                    var ps = priceTimeGetter(tick);
                    volume += Math.Abs(ps.Size);
                    amount += ps.Price * Math.Abs(ps.Size); // sign indicates buy(+) / sell(-)
                }
                return amount / volume;
            });
        }
    }
}
