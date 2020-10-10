//==============================================================================
// Copyright (c) 2017-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financier.Trading
{
    public enum OrderState
    {
        Outstanding,

        Ordering,
        Ordered,
        OrderFailed,

        Executing,  // Exclude conditional order
        Executed,   // Exclude conditional order

        Canceling,
        Canceled,
        CancelFailed,

        Expired,

        Completed,  // Conditional order only
        Triggered,  // Conditional order only
    }

    public static class OrderStateExtension
    {
        public static bool IsClosed(this OrderState state)
        {
            switch (state)
            {
                case OrderState.OrderFailed:
                case OrderState.Executed:
                case OrderState.Canceled:
                case OrderState.CancelFailed:
                case OrderState.Completed:
                case OrderState.Expired:
                    return true;

                default:
                    return false;
            }
        }
    }
}
