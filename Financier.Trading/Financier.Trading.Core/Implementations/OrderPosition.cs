//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading;

public class OrderPosition
{
    public virtual DateTime OpenTime { get; }
    public virtual decimal OpenPrice { get; }
    public virtual DateTime? CloseTime { get; }
    public virtual decimal? ClosePrice { get; }
    public virtual decimal Size { get; }
    public virtual decimal? Profit { get; }
    public virtual decimal? Commission { get; }

    public OrderExecution OpenExecution { get; }
    public IReadOnlyCollection<OrderExecution> CloseExecutions { get; }
}
