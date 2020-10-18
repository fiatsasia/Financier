//==============================================================================
// Copyright (c) 2017-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
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
