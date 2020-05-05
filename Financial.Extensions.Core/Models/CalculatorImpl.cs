//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Reactive.Linq;

//*****************************************************************
// These implementations must isolate from Calculator definitions.
//*****************************************************************

namespace Financial.Internals
{
    class DecimalCalculator : Financial.Extensions.ICalculator<decimal>
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
        public decimal Average<TSource>(IEnumerable<TSource> source, Func<TSource, decimal> selector) => source.Average(selector);
        public IObservable<decimal> Average(IObservable<decimal> source) => source.Average();

        public decimal Sum(IEnumerable<decimal> source) => source.Sum();
        public decimal Sum<TSource>(IEnumerable<TSource> source, Func<TSource, decimal> selector) => source.Sum(selector);
    }

    class DoubleCalculator : Financial.Extensions.ICalculator<double>
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
        public double Average<TSource>(IEnumerable<TSource> source, Func<TSource, double> selector) => source.Average(selector);
        public IObservable<double> Average(IObservable<double> source) => source.Average();

        public double Sum(IEnumerable<double> source) => source.Sum();
        public double Sum<TSource>(IEnumerable<TSource> source, Func<TSource, double> selector) => source.Sum(selector);
    }

    class FloatCalculator : Financial.Extensions.ICalculator<float>
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
        public float Average<TSource>(IEnumerable<TSource> source, Func<TSource, float> selector) => source.Average(selector);
        public IObservable<float> Average(IObservable<float> source) => source.Average();

        public float Sum(IEnumerable<float> source) => source.Sum();
        public float Sum<TSource>(IEnumerable<TSource> source, Func<TSource, float> selector) => source.Sum(selector);
    }
}
