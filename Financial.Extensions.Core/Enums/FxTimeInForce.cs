//==============================================================================
// Copyright (c) 2017-2019 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Financial.Extensions
{
    public enum FxTimeInForce
    {
        NotSpecified,
        GTC,            // Good 'Till Canceled
        IOC,            // Immediate or Cancel
        FOK,            // Fill or Kill
    }
}
