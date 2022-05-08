using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financier.Trading
{
    public class ExchangeFactory<TAdapter> : IDisposable where TAdapter : ExchangeAdapterBase, new()
    {
        TAdapter _adapter;

        public ExchangeFactory()
        {
            _adapter = new TAdapter();
        }

        public void Dispose()
        {
            _adapter.Dispose();
        }

        public Task<AccountBase> GetAccountAsync(string key, string secret) => _adapter.GetAccountAsync(key, secret);

        public Task<AccountBase> GetAccountAsync() => _adapter.GetAccountAsync();

        public Task<MarketDataSourcesBase> GetMarketDataSourcesAsync(string market) => _adapter.GetMarketDataSourcesAsync(market);
    }
}
