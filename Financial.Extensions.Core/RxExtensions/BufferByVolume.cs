//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/feedex/
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive.Linq;

namespace Financial.Extensions
{
    public static partial class RxExtensions
    {
        // Trade.Split が未解決
#if false
        public static IObservable<IFxOhlcv> BufferWithVolume(this IObservable<IFxTrade> source, decimal volume)
        {
            return Observable.Create<IFxOhlc>(observer =>
            {
                decimal accumulatedSize = 0;
                var trades = new List<IFxTrade>();
                return source.Subscribe(trade =>
                {
                    if (accumulatedSize + trade.Size < volume)
                    {
                        accumulatedSize += trade.Size;
                        trades.Add(trade);
                        return;
                    }

                    var residueVolume = trade.Size;
                    var tickSize = volume - accumulatedSize;
                    while (true)
                    {
                        trades.Add(trade.Split(tickSize));
                        observer.OnNext(new FxOhlc(trades));
                        trades = new List<IFxTrade>();

                        residueVolume -= tickSize;
                        if (residueVolume <= volume)
                        {
                            accumulatedSize = residueVolume;
                            trades.Add(trade.Split(residueVolume));
                            break;
                        }
                        else
                        {
                            tickSize = volume;
                        }
                    }
                },
                observer.OnError,
                observer.OnCompleted);
            });
        }
#endif
    }
}
