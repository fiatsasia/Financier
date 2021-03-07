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
using System.Reactive.Disposables;

namespace Financier
{
    public static partial class RxExtensions
    {
        public static IObservable<IOhlcvv> Ohlcvv(this IObservable<IExecution> source, TimeSpan frameSpan, bool insertMissingFrames = false)
        {
            if (!insertMissingFrames)
            {
                return source
                    .Buffer(e => e.Time, frameSpan)
                    .Select(e => new Ohlcvv(frameSpan, e));
            }

            return Observable.Create<IOhlcvv>(observer =>
            {
                var prev = default(Ohlcvv);
                var disposable = source
                    .Buffer(e => e.Time, frameSpan)
                    .Select(e => new Ohlcvv(frameSpan, e))
                .Subscribe(current =>
                {
                    if (prev == default)
                    {
                        observer.OnNext(current);
                    }
                    else if (current.Start == prev.Start + frameSpan)
                    {
                        observer.OnNext(current);
                    }
                    else
                    {
                        var ohlc = prev;
                        while (true)
                        {
                            ohlc = ohlc.CreateMissingFrame();
                            observer.OnNext(ohlc);
                            if (ohlc.Start == current.Start - frameSpan)
                            {
                                break;
                            }
                        }
                        observer.OnNext(current);
                    }
                    prev = current;
                });

                return Disposable.Create(disposable.Dispose);
            });
        }

        public static IObservable<IOhlcv> Ohlcv(this IObservable<IExecution> source, TimeSpan frameSpan, bool insertMissingFrames = false)
            => Ohlcvv(source, frameSpan, insertMissingFrames).Cast<IOhlcv>();

        public static IObservable<IOhlc> Ohlc(this IObservable<IExecution> source, TimeSpan frameSpan, bool insertMissingFrames = false)
            => Ohlcvv(source, frameSpan, insertMissingFrames).Cast<IOhlc>();

        public static IObservable<IOhlcvv> OhlcvvUpdate(this IObservable<IExecution> source, TimeSpan frameSpan, bool insertMissingFrames = false)
        {
            return Observable.Create<IOhlcvv>(observer =>
            {
                var ohlc = default(Ohlcvv);
                var lastFrame = DateTime.MinValue;
                var disposable = source.Subscribe(exec =>
                {
                    var newFrame = exec.Time.Round(frameSpan);
                    if (newFrame != lastFrame)
                    {
                        if (insertMissingFrames && ohlc != default && newFrame != lastFrame + frameSpan)
                        {
                            while (true)
                            {
                                ohlc = ohlc.CreateMissingFrame();
                                observer.OnNext(ohlc);
                                if (newFrame == ohlc.Start + frameSpan)
                                {
                                    break;
                                }
                            }
                        }

                        lastFrame = newFrame;
                        ohlc = new Ohlcvv(frameSpan);
                    }
                    ohlc.Update(exec.Time, exec.Price, exec.Size);
                    observer.OnNext(ohlc);
                });

                return Disposable.Create(disposable.Dispose);
            });
        }

        public static IObservable<IOhlcv> OhlcvUpdate(this IObservable<IExecution> source, TimeSpan frameSpan, bool insertMissingFrames = false)
            => OhlcvvUpdate(source, frameSpan, insertMissingFrames).Cast<IOhlcv>();

        public static IObservable<IOhlc> OhlcUpdate(this IObservable<IExecution> source, TimeSpan frameSpan, bool insertMissingFrames = false)
            => OhlcvvUpdate(source, frameSpan, insertMissingFrames).Cast<IOhlc>();
    }
}
