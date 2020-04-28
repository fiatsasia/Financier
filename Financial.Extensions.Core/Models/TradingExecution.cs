//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    class TradingExecution : ITradingExecution<double, double>
    {
        public DateTime Time { get; set; }
        public double Price { get; set; }
        public double Size { get; set; }

        public TradingExecution(double executedPrice, double executedSize)
        {

        }
    }
}
