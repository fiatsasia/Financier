//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading;

public enum OrderState
{
    Unknown = -1,

    Outstanding,

    Ordered,
    OrderFailed,
    OrderOpened,

    PartiallyExecuted,  // Exclude conditional order
    Executed,   // Exclude conditional order

    Canceled,
    CancelFailed,

    Expired,

    Completed,  // Conditional order only
    Triggered,  // Conditional order only
}
