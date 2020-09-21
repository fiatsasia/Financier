//==============================================================================
// Copyright (c) 2017-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financier.Trading
{
    public enum OrderState
    {
        Unknown,

        Outstanding,

        Ordering,
        Ordered,
        OrderFailed,

        Executing,
        Executed,

        Canceling,
        Canceled,
        CancelFailed,

        Completed,
        Expired,
    }
}
