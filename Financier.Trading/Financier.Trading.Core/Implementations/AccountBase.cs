using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financier.Trading
{
    public abstract class AccountBase : IDisposable
    {
        public event EventHandler<OrderEventArgs> TransactionChanged;
        public event EventHandler<OrderPositionEventArgs> PositionChanged;

        public virtual void Dispose()
        {
        }

        public abstract Task OpenAsync();
        public abstract Task PlaceOrderAsync(Order order);
        public virtual Task CancelTransactionAsync(Ulid transactionId) => throw new NotSupportedException();
        public virtual Task CancelAllTransactionsAsync() => throw new NotSupportedException();
        public virtual Task CloseAllPositionsAsync() => throw new NotSupportedException();

        protected void OnTransactionChanged(OrderEventArgs e) => TransactionChanged?.Invoke(this, e);

        protected void OnPositionChanged(OrderPositionEventArgs e) => PositionChanged?.Invoke(this, e);
    }
}
