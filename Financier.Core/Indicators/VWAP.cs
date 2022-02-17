//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Financier
{
    public static partial class IndicatorExtensions
    {
        // With selector method
        public static IObservable<double> VWAP<TSource>(this IObservable<IList<TSource>> source, Func<TSource, double> priceSelector, Func<TSource, double> sizeSelector)
        {
            return source.Select(ticks =>
            {
                double volume = 0.0;
                double amount = 0.0;
                foreach (var tick in ticks)
                {
                    volume += Math.Abs(sizeSelector(tick));
                    amount += priceSelector(tick) * Math.Abs(sizeSelector(tick)); // sign indicates buy(+) / sell(-)
                }
                return amount / volume;
            });
        }
    }
}
