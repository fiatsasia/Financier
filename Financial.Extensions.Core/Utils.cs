//==============================================================================
// Copyright (c) 2013-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Reactive.Disposables;

namespace Financial.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Round(this DateTime dt, TimeSpan unit)
        {
            return new DateTime(dt.Ticks / unit.Ticks * unit.Ticks, dt.Kind);
        }
    }

    public static class RxUtil
    {
        public static TResult AddTo<TResult>(this TResult resource, CompositeDisposable disposable) where TResult : IDisposable
        {
            disposable.Add(resource);
            return resource;
        }
    }
}
