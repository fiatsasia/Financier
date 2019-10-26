﻿//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Reactive.Linq;

namespace Financial.Extensions
{
    public static partial class Indicators
    {
        /// <summary>
        /// Accumulation distribution (ADL)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IObservable<double> AccumulationDistribution(this IObservable<IFxOhlcv> source)
        {
            return source
                // Calculate money flow volume
                .Select(ohlc =>
                {
                    return unchecked((double)(((ohlc.Close - ohlc.Low) - (ohlc.High - ohlc.Close)) / (ohlc.High - ohlc.Low) * (unchecked((decimal)ohlc.Volume))));
                })
                // Accummulate previous and current
                .Scan(double.NaN, (prev, current) =>
                {
                    return double.IsNaN(prev) ? current : prev + current;
                });
        }
    }
}