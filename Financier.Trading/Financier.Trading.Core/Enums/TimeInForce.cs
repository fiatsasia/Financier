//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading
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
