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

namespace Financier.Signals
{
    public static partial class SignalExtensions
    {
        /// <summary>
        /// Replace same side signals to steady (signal = 0)
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TPrice"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IObservable<ISignal<TSource, TPrice>> SuplessDuplicates<TSource, TPrice>(this IObservable<ISignal<TSource, TPrice>> source)
        {
            return source.StartWith(default(ISignal<TSource, TPrice>)).Buffer(2, 1).Where(e => e.Count >= 2).Select(signals =>
            {
                if (signals[0] == null)
                {
                    return signals[1];
                }

                // If Steady state when after signaled, copy signal to Steady.
                if (signals[0].Signal != 0 && signals[1].Signal == 0)
                {
                    signals[1].Signal = signals[0].Signal;
                }

                if (signals[0].Signal == signals[1].Signal && signals[1].Signal != 0)
                {
                    var result = signals[1].Clone();
                    result.Signal = 0;
                    return result;
                }

                return signals[1];
            });
        }
    }
}
