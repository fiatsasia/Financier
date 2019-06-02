//==============================================================================
// Copyright (c) 2017-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Financial.Extensions
{
    public enum FxOrderType
    {
        Unspecified,

        // Simple order types
        LimitPrice,
        MarketPrice,

        // Conditioned order types
        Stop,
        StopLimit,
        TrailingStop,

        // Combined order types
        IFD,
        OCO,
        IFDOCO,
    }
}
