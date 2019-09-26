//==============================================================================
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
        /// Chaikin oscillator (CHO)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IObservable<double> ChaikinOscillator(this IObservable<IFxOhlcv> source, int shortPeriod = 3, int longPeriod = 10)
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
