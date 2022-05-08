using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financier.Trading
{
    public abstract class AccountBase : IDisposable
    {
        public event EventHandler<OrderTransactionEventArgs> TransactionChanged;
        public event EventHandler<OrderPositionEventArgs> PositionChanged;

        public virtual void Dispose()
        {
        }

        public abstract Task OpenAsync();
        public abstract Task OpenAsync(string key, string secret);
        public abstract Task PlaceOrderAsync(string product, OrderRequest order);
        public virtual Task CancelTransactionAsync(Ulid transactionId) => throw new NotSupportedException();
        public virtual Task CancelAllTransactionsAsync() => throw new NotSupportedException();
        public virtual Task CloseAllPositionsAsync() => throw new NotSupportedException();

        protected void OnTransactionChanged(OrderTransactionEventArgs e) => TransactionChanged?.Invoke(this, e);

        protected void OnPositionChanged(OrderPositionEventArgs e) => PositionChanged?.Invoke(this, e);
    }
}
