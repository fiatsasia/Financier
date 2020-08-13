//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financier
{
    public enum TimeInForce
    {
        NotSpecified,
        GTC,            // Good 'Till Canceled
        IOC,            // Immediate or Cancel
        FOK,            // Fill or Kill
        DAY,            // Today's trading hours
        GTD,            // Good 'Till Date
        OPG,            // Opening session
        CLO,            // Closing session
    }
}
