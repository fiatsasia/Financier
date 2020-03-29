//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Reactive.Linq;

namespace Financial.Extensions.Indicators
{
    public static partial class IndicatorExtensions
    {
        /// <summary>
        /// Chaikin oscillator (CHO)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IObservable<double> ChaikinOscillator(this IObservable<IOhlcv<double>> source, int shortPeriod = 3, int longPeriod = 10)
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
