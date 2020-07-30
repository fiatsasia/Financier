//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public class Execution : IExecution
    {
        public DateTime Time { get; set; }
        public decimal Price { get; set; }
        public decimal Size { get; set; }
    }
}
