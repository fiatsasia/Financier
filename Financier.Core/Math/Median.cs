//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.FinMath;

public static class MathExtensions
{
    public static double Median<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
    {
        var sorted = source.OrderBy(selector).ToList();
        if (sorted.Count % 2 == 1)
        {
            return selector(sorted[sorted.Count / 2]);
        }
        else
        {
            return (selector(sorted[sorted.Count / 2 - 1]) + selector(sorted[sorted.Count / 2])) / 2.0d;
        }
    }
}
