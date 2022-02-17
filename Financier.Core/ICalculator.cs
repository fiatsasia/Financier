//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Collections.Generic;

namespace Financier
{
    public interface ICalculator<TValue>
    {
        TValue MaxValue { get; }
        TValue MinValue { get; }
        TValue Zero { get; }

        int CompareTo(TValue left, TValue right);
        int Sign(TValue value);

        decimal ToDecimal(TValue value);
        double ToDouble(TValue value);
        float ToFloat(TValue value);

        TValue Cast(decimal value);
        TValue Cast(double value);
        TValue Cast(float value);

        TValue Add(params TValue[] values);
        TValue Sub(TValue value1, TValue value2);
        TValue Mul(TValue value1, TValue value2);
        TValue Div(TValue value1, TValue value2);

        TValue Invert(TValue value);
        TValue Abs(TValue value);

        TValue Average(IEnumerable<TValue> source);
        TValue Average<TSource>(IEnumerable<TSource> source, Func<TSource, TValue> selector);
        IObservable<TValue> Average(IObservable<TValue> source);

        TValue Sum(IEnumerable<TValue> source);
        TValue Sum<TSource>(IEnumerable<TSource> source, Func<TSource, TValue> selector);
    }
}
