//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public class CrossoverSignal<TSource, TPrice> : ICrossoverSignal<TSource, TPrice>
    {
        // ITradingSignal<TPrice>
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int Signal { get; set; }     // 0:none, 1:buy, -1:sell
        public TPrice Price { get; set; }

        // ITradingSignal<TSource, TPrice>
        public TSource Source { get; set; }

        // ICrossoverSignal<TPrice>
        public TPrice BasePrice { get; set; }

        public ITradingSignal<TSource, TPrice> Clone()
        {
            return new CrossoverSignal<TSource, TPrice>
            {
                Id = Id,
                Time = Time,
                Signal = Signal,
                Price = Price,
                Source = Source,
                BasePrice = BasePrice,
            };
        }
    }
}
