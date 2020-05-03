//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Financial.Extensions
{
    class DecimalCalculator : ICalculator<decimal>
    {
        public decimal MaxValue => decimal.MaxValue;
        public decimal MinValue => decimal.MinValue;
        public decimal Zero => decimal.Zero;

        public int CompareTo(decimal left, decimal right) => left.CompareTo(right);
        public int Sign(decimal value) => Math.Sign(value);

        public decimal ToDecimal(decimal value) => value;
        public double ToDouble(decimal value) => unchecked((double)value);
        public float ToFloat(decimal value) => unchecked((float)value);

        public decimal Cast(decimal value) => value;
        public decimal Cast(double value) => unchecked((decimal)value);
        public decimal Cast(float value) => unchecked((decimal)value);

        public decimal Add(params decimal[] values) => values.Sum();
        public decimal Sub(decimal value1, decimal value2) => value1 - value2;
        public decimal Mul(decimal value1, decimal value2) => value1 * value2;
        public decimal Div(decimal value1, decimal value2) => value1 / value2;

        public decimal Invert(decimal value) => -value;
        public decimal Abs(decimal value) => Math.Abs(value);

        public decimal Average(IEnumerable<decimal> source) => source.Average();
        public decimal Average<TSource>(IEnumerable<TSource> source, Func<TSource, decimal> selector) => source.Average(e => selector(e));
        public IObservable<decimal> Average(IObservable<decimal> source) => source.Average();

        public decimal Sum<TSource>(IEnumerable<TSource> source, Func<TSource, decimal> selector) => source.Sum(e => selector(e));
    }

    class DoubleCalculator : ICalculator<double>
    {
        public double MaxValue => double.MaxValue;
        public double MinValue => double.MinValue;
        public double Zero => 0.0d;

        public int CompareTo(double left, double right) => left.CompareTo(right);
        public int Sign(double value) => Math.Sign(value);

        public decimal ToDecimal(double value) => unchecked((decimal)value);
        public double ToDouble(double value) => value;
        public float ToFloat(double value) => unchecked((float)value);

        public double Cast(decimal value) => unchecked((double)value);
        public double Cast(double value) => value;
        public double Cast(float value) => value;

        public double Add(params double[] values) => values.Sum();
        public double Sub(double value1, double value2) => value1 - value2;
        public double Mul(double value1, double value2) => value1 * value2;
        public double Div(double value1, double value2) => value1 / value2;

        public double Invert(double value) => -value;
        public double Abs(double value) => Math.Abs(value);

        public double Average(IEnumerable<double> source) => source.Average();
        public double Average<TSource>(IEnumerable<TSource> source, Func<TSource, double> selector) => source.Average(e => selector(e));
        public IObservable<double> Average(IObservable<double> source) => source.Average();

        public double Sum<TSource>(IEnumerable<TSource> source, Func<TSource, double> selector) => source.Sum(e => selector(e));
    }

    class FloatCalculator : ICalculator<float>
    {
        public float MaxValue => float.MaxValue;
        public float MinValue => float.MinValue;
        public float Zero => 0.0f;

        public int CompareTo(float left, float right) => left.CompareTo(right);
        public int Sign(float value) => Math.Sign(value);

        public decimal ToDecimal(float value) => unchecked((decimal)value);
        public double ToDouble(float value) => value;
        public float ToFloat(float value) => value;

        public float Cast(decimal value) => unchecked((float)value);
        public float Cast(double value) => unchecked((float)value);
        public float Cast(float value) => value;

        public float Add(params float[] values) => values.Sum();
        public float Sub(float value1, float value2) => value1 - value2;
        public float Mul(float value1, float value2) => value1 * value2;
        public float Div(float value1, float value2) => value1 / value2;

        public float Invert(float value) => -value;
        public float Abs(float value) => Math.Abs(value);

        public float Average(IEnumerable<float> source) => source.Average();
        public float Average<TSource>(IEnumerable<TSource> source, Func<TSource, float> selector) => source.Average(e => selector(e));
        public IObservable<float> Average(IObservable<float> source) => source.Average();

        public float Sum<TSource>(IEnumerable<TSource> source, Func<TSource, float> selector) => source.Sum(e => selector(e));
    }

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

        public static T Average<T>(IEnumerable<T> source) => Get<T>().Average(source);
        public static T Average<TSource, T>(IEnumerable<TSource> source, Func<TSource, T> selector) => Get<T>().Average(source, selector);
        public static IObservable<T> Average<T>(IObservable<T> source) => Get<T>().Average(source);

        public static T Sum<TSource, T>(IEnumerable<TSource> source, Func<TSource, T> selector) => Get<T>().Sum(source, selector);
    }
}
