//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;

namespace Financier.Signals
{
    class CrossoverSignal<TSource, TPrice> : ICrossoverSignal<TSource, TPrice>
    {
        // ISignal<TPrice>
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int Signal { get; set; }     // 0:none, 1:buy, -1:sell
        public TPrice TriggerPrice { get; set; }

        // ISignal<TSource, TPrice>
        public TSource Source { get; set; }

        // ICrossoverSignal<TPrice>
        public TPrice BasePrice { get; set; }

        public ISignal<TSource, TPrice> Clone()
        {
            return new CrossoverSignal<TSource, TPrice>
            {
                Id = Id,
                Time = Time,
                Signal = Signal,
                TriggerPrice = TriggerPrice,
                BasePrice = BasePrice,
                Source = Source,
            };
        }
    }
}
