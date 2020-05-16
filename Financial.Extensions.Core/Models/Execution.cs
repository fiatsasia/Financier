//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions.Trading
{
    public class Execution<TPrice, TSize> : IExecution<TPrice, TSize>
    {
        public DateTime Time { get; set; }
        public TPrice Price { get; set; }
        public TSize Size { get; set; }
    }
}
