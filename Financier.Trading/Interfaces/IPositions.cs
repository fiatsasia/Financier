//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
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
