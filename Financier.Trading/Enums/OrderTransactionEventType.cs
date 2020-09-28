//==============================================================================
// Copyright (c) 2017-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financier.Trading
{
    public enum OrderTransactionEventType
    {
        Unknown,

        OrderSending,
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

        ChildOrderEvent,
    }

    public static class OrderTransactionEventTypeExtension
    {
        public static bool IsClosed(this OrderTransactionEventType eventType)
        {
            switch (eventType)
            {
                case OrderTransactionEventType.OrderSendFailed:
                case OrderTransactionEventType.OrderSendCanceled:
                case OrderTransactionEventType.OrderFailed:
                case OrderTransactionEventType.Executed:
                case OrderTransactionEventType.Completed:
                case OrderTransactionEventType.CancelSendFailed:
                case OrderTransactionEventType.CancelSendCanceled:
                case OrderTransactionEventType.Canceled:
                case OrderTransactionEventType.CancelFailed:
                case OrderTransactionEventType.Expired:
                    return true;

                default:
                    return false;
            }
        }
    }
}
