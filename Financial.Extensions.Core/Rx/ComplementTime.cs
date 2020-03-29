//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Reactive.Linq;

namespace Financial.Extensions.Rx
{
    public static partial class RxExtensions
    {
        public static IObservable<TSource> ComplementPeriods<TSource>(
            this IObservable<TSource> source,
            Func<TSource, DateTime> getTime,
            Func<DateTime, TSource, TSource> createComplement,
            int timeIntervalMinute
        )
        {
            return Observable.Create<TSource>(observer =>
            {
                source.Buffer(2, 1).Subscribe(e =>
                {
                    if (e.Count < 2)
                    {
                        observer.OnNext(e[0]);
                        return;
                    }

                    var fromTime = getTime(e[0]);
                    var toTime = getTime(e[1]);
                    if (fromTime + TimeSpan.FromMinutes(timeIntervalMinute) == toTime)
                    {
                        observer.OnNext(e[0]);
                        return;
                    }

                    var complementIntervalCount = ((toTime - fromTime).TotalMinutes - 1) / timeIntervalMinute;
                    observer.OnNext(e[0]);
                    for (int i = 1; i <= complementIntervalCount; i++)
                    {
                        observer.OnNext(createComplement(fromTime.AddMinutes(timeIntervalMinute * i), e[0]));
                    }
                });

                return () => { };
            });
        }
    }
}
