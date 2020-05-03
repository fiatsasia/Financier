//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions.Trading
{
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
}
