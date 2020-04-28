//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    // Entry / Exit signal の取り扱い

    public interface ITradingSignal
    {
        DateTime Time { get; }
        int Signal { get; set; }     // 0:none, 1:buy, -1:sell
    }

    public interface ITradingSignal<TPrice> : ITradingSignal
    {
        TPrice Price { get; }
    }

    public interface ICrossoverSignal<TPrice> : ITradingSignal<TPrice>
    {
        TPrice BasePrice { get; }
    }

    public interface ITradingSignal<TSource, TPrice> : ITradingSignal<TPrice>
    {
        TSource Source { get; }
        ITradingSignal<TSource, TPrice> Clone();
    }


    public interface ICrossoverSignal<TSource, TPrice> : ITradingSignal<TSource, TPrice>
    {
        TPrice BasePrice { get; }
    }
}
