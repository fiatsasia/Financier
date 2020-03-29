﻿//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financial.Extensions
{
    public enum TimeInForce
    {
        NotSpecified,
        GTC,            // Good 'Till Canceled
        IOC,            // Immediate or Cancel
        FOK,            // Fill or Kill
    }
}