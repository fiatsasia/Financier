//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading
{
    public enum OrderState
    {
        Outstanding,

        Ordered,
        OrderFailed,

        PartiallyExecuted,  // Exclude conditional order
        Executed,   // Exclude conditional order

        Canceled,
        CancelFailed,

        Expired,

        Completed,  // Conditional order only
        Triggered,  // Conditional order only
    }

    public static class OrderStateExtension
    {
        public static bool IsClosed(this OrderState state)=> state switch
        {
            OrderState.OrderFailed => true,
            OrderState.Executed => true,
            OrderState.Canceled => true,
            OrderState.CancelFailed => true,
            OrderState.Completed => true,
            OrderState.Expired => true,
            _ => false
        };

        public static bool IsCancelable(this OrderState state) => state switch
        {
            OrderState.Ordered => true,
            OrderState.PartiallyExecuted => true,
            OrderState.Triggered => true,

            OrderState.Outstanding => false,
            OrderState.OrderFailed => false,
            OrderState.Executed => false,
            OrderState.Canceled => false,
            OrderState.CancelFailed => false,
            OrderState.Expired => false,
            OrderState.Completed => false,

            _ => false
        };
    }
}
