//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Collections.Generic;

namespace Financier.Trading
{
    public interface IOrderEntity
    {
        Ulid Id { get; }
        string ProductCode { get; }
        OrderType OrderType { get; }
        OrderState Status { get; }
        decimal? Size { get; }
        decimal? Price1 { get; }
        decimal? Price2 { get; }
        DateTime? OpenTime { get; }
        DateTime? CloseTime { get; }
        Ulid? ParentId { get; }
        DateTime ExpirationDate { get; }
        Ulid[] ChildOrderIds { get; }
        IReadOnlyDictionary<string, object> Metadata { get; }
    }

    public interface IOrderEntity<TExecution> : IOrderEntity where TExecution : IExecutionEntity
    {
        IReadOnlyList<TExecution> Executions { get; }
    }
}
