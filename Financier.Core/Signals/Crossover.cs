//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Signals;

public static partial class SignalExtensions
{
    public static IObservable<int> Crossover(this IObservable<double> spsource, IObservable<double> lpsource, Func<double, double, bool> thresholdComparer)
    {
        return Observable.Create<int>(observer =>
        {
            var lastSignal = 0;

            return spsource.Publish(oo1 => lpsource.Select(o2 => oo1.Select(o1 => new Tuple<double, double>(o1, o2))).Switch())
            .Subscribe(spplpp =>
            {
                var diffRate = (spplpp.Item1 - spplpp.Item2) / spplpp.Item2;
                var signal = Math.Sign(diffRate);

                if (signal != 0 && signal != lastSignal && thresholdComparer(spplpp.Item1, spplpp.Item2))
                {
                    lastSignal = signal;
                }
                else
                {
                    signal = 0;
                }

                if (signal != 0)
                {
                    observer.OnNext(signal);
                }
            },
            observer.OnError,
            observer.OnCompleted);
        });
    }

    /// <summary>
    /// Returns trade signal from short/long moving average combinations.
    /// </summary>
    /// <param name="spsource">Short period moving average source</param>
    /// <param name="lpsource">Long period moving average source</param>
    /// <param name="threshold">Specify threshold of short/long rate of difference</param>
    /// <returns>1:Buy, -1:Sell, 0:Not signaled</returns>
    public static IObservable<int> Crossover(this IObservable<double> spsource, IObservable<double> lpsource, double threshold)
    {
        return spsource.Crossover(lpsource, (spp, lpp) => { return Math.Abs((spp - lpp) / lpp) >= threshold; });
    }
}
