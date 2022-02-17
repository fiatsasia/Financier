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
    public interface IPositionEntity
    {
        Ulid Id { get; }
        Ulid OpenExecutionId { get; }
        int OpenExecutionIndex { get; }
        Ulid? CloseExecutionId { get; }
        int? CloseExecutionIndex { get; }
        decimal Size { get; }
        decimal? Profit { get; }
        decimal? Commission { get; }
        IReadOnlyDictionary<string, object> Metadata { get; }
    }
}
