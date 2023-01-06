//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier;

public static partial class IndicatorExtensions
{
    /// <summary>
    /// Accumulation distribution (ADL)
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IObservable<double> AccumulationDistribution(this IObservable<IOhlcv> source)
    {
        return source
            // Calculate money flow volume
            .Select(ohlc =>
            {
                return Convert.ToDouble(((ohlc.Close - ohlc.Low) - (ohlc.High - ohlc.Close)) / (ohlc.High - ohlc.Low) * ohlc.Volume);
            })
            // Accummulate previous and current
            .Scan(double.NaN, (prev, current) =>
            {
                return double.IsNaN(prev) ? current : prev + current;
            });
    }
}
