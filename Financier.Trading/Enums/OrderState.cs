//==============================================================================
// Copyright (c) 2017-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
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
