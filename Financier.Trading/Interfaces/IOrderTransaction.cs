using System;
using System.Collections.Generic;
using System.Text;

namespace Financier.Trading
{
    public interface IOrderTransaction
    {
        string Id { get; }
        OrderTransactionState State { get; }
        IOrder Order { get; }

        bool IsCancelable { get; }

        DateTime OpenTime { get; }

        void Cancel();
    }
}
