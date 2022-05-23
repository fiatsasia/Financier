//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading
{
    public enum OrderEventType
    {
        Unknown,

        StartOrdering,
        Ordered,
        OrderSendFailed,
        OrderSendCanceled,
        OrderOpened,
        OrderFailed,

        PartiallyExecuted,
        Executed,
        Triggered,
        Completed,

        CancelSending,
        CancelSent,
        CancelSendFailed,
        CancelSendCanceled,
        Canceled,
        CancelFailed,

        Expired,
    }

    public static class OrderEventTypeExtension
    {
        public static bool IsClosed(this OrderEventType eventType) => eventType switch
        {
            OrderEventType.OrderSendFailed => true,
            OrderEventType.OrderSendCanceled => true,
            OrderEventType.OrderFailed => true,
            OrderEventType.Executed => true,
            OrderEventType.Completed => true,
            OrderEventType.CancelSendFailed => true,
            OrderEventType.CancelSendCanceled => true,
            OrderEventType.Canceled => true,
            OrderEventType.CancelFailed => true,
            OrderEventType.Expired => true,
            _ => false
        };
    }
}
