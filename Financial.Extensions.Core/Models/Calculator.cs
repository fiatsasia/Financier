//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Reactive.Linq;

using Financial.Internals;

namespace Financial.Extensions
{
    // class IntCalculator
    // 利益計算を行う際、利益はプラス方向、損失はマイナス方向へ丸める(Floor)にする必要がある。
    // 日本円計算のための利用が前提

    public static class Calculator
    {
        static Dictionary<Type, object> _dic = new Dictionary<Type, object>
        {
            { typeof(decimal), new DecimalCalculator() },
            { typeof(double), new DoubleCalculator() },
            { typeof(float), new FloatCalculator() },
        };

        public static void Register<T>(ICalculator<T> calculator)
        {
            _dic[typeof(T)] = calculator;
        }

        public static ICalculator<T> Get<T>()
        {
            return (ICalculator<T>)_dic[typeof(T)];
        }

        public static T MinValue<T>() => Get<T>().MinValue;
        public static T MaxValue<T>() => Get<T>().MaxValue;
        public static T Zero<T>() => Get<T>().Zero;

        public static T Add<T>(params T[] values) => Get<T>().Add(values);
        public static T Sub<T>(T value1, T value2) => Get<T>().Sub(value1, value2);
        public static T Mul<T>(T value1, T value2) => Get<T>().Mul(value1, value2);
        public static T Div<T>(T value1, T value2) => Get<T>().Div(value1, value2);

        public static T Cast<T>(decimal value) => Get<T>().Cast(value);
        public static T Cast<T>(double value) => Get<T>().Cast(value);
        public static T Cast<T>(float value) => Get<T>().Cast(value);

        public static int CompareTo<T>(T left, T right) => Get<T>().CompareTo(left, right);
        public static int Sign<T>(T value) => Get<T>().Sign(value);
        public static T Invert<T>(T value) => Get<T>().Invert(value);
        public static T Abs<T>(T value) => Get<T>().Abs(value);

        public static decimal ToDecimal<T>(T value) => Get<T>().ToDecimal(value);
        public static double ToDouble<T>(T value) => Get<T>().ToDouble(value);

        public static TValue Average<TValue>(this IEnumerable<TValue> source) => Get<TValue>().Average(source);
        public static TValue Average<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector) => Get<TValue>().Average(source, selector);
        public static IObservable<TValue> Average<TValue>(this IObservable<TValue> source) => Get<TValue>().Average(source);

        public static TValue Sum<TValue>(this IEnumerable<TValue> source) => Get<TValue>().Sum(source);
        public static TValue Sum<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector) => Get<TValue>().Sum(source, selector);

        public static T Median<TSource, T>(this IEnumerable<TSource> source, Func<TSource, T> selector)
        {
            var sorted = source.OrderBy(selector).ToList();
            if (sorted.Count % 2 == 1)
            {
                return selector(sorted[sorted.Count / 2]);
            }
            else
            {
                return Div((Add(selector(sorted[sorted.Count / 2 - 1]), selector(sorted[sorted.Count / 2]))), Cast<T>(2.0));
            }
        }
    }
}
