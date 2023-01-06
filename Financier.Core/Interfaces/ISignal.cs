//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Signals;

// Entry / Exit signal の取り扱い

public interface ISignal
{
    DateTime Time { get; }
    int Signal { get; set; }     // 0:none, 1:buy, -1:sell
}

public interface ISignal<TPrice> : ISignal
{
    TPrice TriggerPrice { get; }
}

public interface ICrossoverSignal<TPrice> : ISignal<TPrice>
{
    TPrice BasePrice { get; }
}

public interface ISignal<TSource, TPrice> : ISignal<TPrice>
{
    TSource Source { get; }
    ISignal<TSource, TPrice> Clone();
}


public interface ICrossoverSignal<TSource, TPrice> : ISignal<TSource, TPrice>
{
    TPrice BasePrice { get; }
}
