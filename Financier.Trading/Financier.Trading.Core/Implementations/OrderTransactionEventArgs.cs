using System;
using System.Collections.Generic;
using System.Text;

namespace Financier.Trading
{
    public class OrderTransactionEventArgs : EventArgs
    {
        /*
        Ulid Id { get; }
        OrderType OrderType { get; }
        decimal? OrderPrice { get; }
        decimal? OrderSize { get; }
        int ExecutionIndex { get; }
        decimal? ExecutedPrice { get; }
        decimal? ExecutedSize { get; }
        decimal? TriggerPrice { get; }

        */

        public DateTime Time { get; }
        public OrderTransactionEventType EventType { get; }
        public OrderType OrderType => _tx.Order.OrderType;
        public OrderTransactionBase Transaction => _tx;
        public OrderExecution Execution { get; }

        OrderTransactionBase _tx;

        public OrderTransactionEventArgs(DateTime time, OrderTransactionEventType eventType, OrderTransactionBase tx)
        {
            Time = time;
            EventType = eventType;
            _tx = tx;
        }

        public OrderTransactionEventArgs(DateTime time, OrderTransactionEventType eventType, OrderTransactionBase tx, OrderExecution exec)
        {
            Time = time;
            EventType = eventType;
            _tx = tx;
            Execution = exec;
        }
    }
}
