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
    public interface IOrderEntity
    {
        Ulid Id { get; }
        OrderType OrderType { get; }
        OrderState Status { get; }
        decimal? Size { get; }
        decimal? Price1 { get; }
        decimal? Price2 { get; }
        DateTime? OpenTime { get; }
        DateTime? CloseTime { get; }
        Ulid? ParentId { get; }
        DateTime ExpirationDate { get; }
        string Metadata { get; }
    }
}
