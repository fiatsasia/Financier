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
    public interface IPositions
    {
        decimal TotalOpenSize { get; }
        IEnumerable<IPosition> GetOpenPositions();
    }
}
