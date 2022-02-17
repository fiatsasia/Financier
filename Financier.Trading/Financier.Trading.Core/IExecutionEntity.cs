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
    public interface IExecutionEntity
    {
        Ulid Id { get; }
        int Index { get; }
        DateTime Time { get; }
        decimal Size { get; }
        decimal Price { get; }
        decimal Commission { get; }
        IReadOnlyDictionary<string, object> Metadata { get; }
    }
}
