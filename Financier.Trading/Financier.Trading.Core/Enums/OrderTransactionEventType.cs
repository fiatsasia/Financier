//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading
{
    public enum OrderTransactionEventType
    {
        Unknown,

        StartOrdering,
        OrderSent,
        OrderSendFailed,
        OrderSendCanceled,
        Ordered,
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

    public static class OrderTransactionEventTypeExtension
    {
        public static bool IsClosed(this OrderTransactionEventType eventType) => eventType switch
        {
            OrderTransactionEventType.OrderSendFailed => true,
            OrderTransactionEventType.OrderSendCanceled => true,
            OrderTransactionEventType.OrderFailed => true,
            OrderTransactionEventType.Executed => true,
            OrderTransactionEventType.Completed => true,
            OrderTransactionEventType.CancelSendFailed => true,
            OrderTransactionEventType.CancelSendCanceled => true,
            OrderTransactionEventType.Canceled => true,
            OrderTransactionEventType.CancelFailed => true,
            OrderTransactionEventType.Expired => true,
            _ => false
        };
    }
}
