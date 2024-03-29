﻿//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading;

public class OrderExecution
{
    public DateTime Time { get; }
    public decimal Price { get; }
    public decimal Size { get; }
    public decimal? Commission { get; }

    public OrderExecution(DateTime time, decimal price, decimal size, decimal commission)
    {
        Time = time;
        Price = price;
        Size = size;
        Commission = commission;
    }

    public OrderExecution(DateTime time, decimal price, decimal size)
    {
        Time = time;
        Price = price;
        Size = size;
    }
}
