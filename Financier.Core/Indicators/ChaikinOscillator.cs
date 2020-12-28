//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
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
        /// <summary>
        /// Chaikin oscillator (CHO)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IObservable<double> ChaikinOscillator(
            this IObservable<IOhlcv> source,
            int shortPeriod = 3,
            int longPeriod = 10
        )
        {
            return source.Publish(
                s => s.AccumulationDistribution().ExponentialMovingAverage(shortPeriod)
                .Zip(s.AccumulationDistribution().ExponentialMovingAverage(longPeriod),
            (sp, lp) =>
            {
                return sp - lp;
            }));
        }
    }
}
