//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
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
