//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financial.Extensions
{
    public interface ICalculator<T>
    {
        T MaxValue { get; }
        T MinValue { get; }
        T Zero { get; }

        int CompareTo(T left, T right);
        int Sign(T value);

        decimal ToDecimal(T value);
        double ToDouble(T value);
        float ToFloat(T value);

        T Cast(decimal value);
        T Cast(double value);
        T Cast(float value);

        T Add(T value1, T value2);
        T Sub(T value1, T value2);
        T Mul(T value1, T value2);
        T Div(T value1, T value2);

        T Invert(T value);
        T Abs(T value);

        T Average(IEnumerable<T> source);
        T Average<TSource>(IEnumerable<TSource> source, Func<TSource, T> selector);
        IObservable<T> Average(IObservable<T> source);
    }
}
