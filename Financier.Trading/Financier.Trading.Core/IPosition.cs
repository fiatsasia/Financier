//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;

namespace Financier.Trading
{
    public interface IPosition : IPositionEntity
    {
        bool IsOpened { get; }
        DateTime OpenTime { get; }
        DateTime? CloseTime { get; }
        decimal OpenPrice { get; }
        decimal? ClosePrice { get; }

        IPositions Positions { get; }
    }
}
