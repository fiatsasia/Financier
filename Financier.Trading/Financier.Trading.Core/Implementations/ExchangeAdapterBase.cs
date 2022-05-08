using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financier.Trading
{
    public abstract class ExchangeAdapterBase : IDisposable
    {
        public virtual void Dispose() { }
        public virtual Task<AccountBase> GetAccountAsync() => throw new NotSupportedException();
        public virtual Task<AccountBase> GetAccountAsync(string key, string secret) => throw new NotSupportedException();
        public virtual Task<MarketDataSourcesBase> GetMarketDataSourcesAsync(string product) => throw new NotSupportedException();
    }
}
