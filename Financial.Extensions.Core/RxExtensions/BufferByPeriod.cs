using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Financial.Extensions
{
    public static partial class RxExtensions
    {
        // With getter method
        public static IObservable<IList<TSource>> BufferByPeriod<TSource>(this IObservable<TSource> source, Func<TSource, DateTime> timeGetter, TimeSpan period)
        {
            var duration = new Subject<Unit>();
            return source
            .Scan((prev, current) =>
            {
                if (timeGetter(prev).Round(period) != timeGetter(current).Round(period))
                {
                    duration.OnNext(Unit.Default);
                }
                return current;
            })
            .Buffer(() => duration);
        }

        // Common trade class
        public static IObservable<IList<TSource>> BufferByPeriod<TSource>(this IObservable<TSource> source, TimeSpan period) where TSource : IFxTradeStream
        {
            var duration = new Subject<Unit>();
            return source
            .Scan((prev, current) =>
            {
                if (prev.Time.Round(period) != current.Time.Round(period))
                {
                    duration.OnNext(Unit.Default);
                }
                return current;
            })
            .Buffer(() => duration);
        }
    }
}
