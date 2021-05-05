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
    public interface IPositionEntity
    {
        Ulid Id { get; }
        Ulid OpenExecutionId { get; }
        int OpenExecutionIndex { get; }
        Ulid? CloseExecutionId { get; }
        int? CloseExecutionIndex { get; }
        decimal Size { get; }
        string Metadata { get; }
    }
}
