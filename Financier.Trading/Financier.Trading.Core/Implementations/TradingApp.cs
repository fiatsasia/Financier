//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading;

public class TradingApp : ITradingApp
{
    public event Func<ITradingApp, Task> Initialized;
    public event Func<ITradingApp, Task> Opened;

    public IEnumerable<OrderTransactionBase> Transactions => _transactions.Values;

    Dictionary<string, object> _config = new();
    MarketBase _market;

    readonly ConcurrentDictionary<Ulid, OrderTransactionBase> _transactions = new();
    Func<IReadOnlyDictionary<string, object>, AccountBase> _orderHandlerloader;
    Func<IReadOnlyDictionary<string, object>, IRealtimeSource> _realtimeSourceloader;
    Func<IReadOnlyDictionary<string, object>, IHistoricalSource> _historicalSourceloader;

    public TradingApp()
    {
    }

    public TradingApp(IReadOnlyDictionary<string, object> configuration)
    {
        configuration.ForEach(e => _config.Add(e.Key, e.Value));
    }

    public virtual void Dispose()
    {
    }

    public ValueTask DisposeAsync()
    {
        return new ValueTask(Task.Run(() => Dispose()));
    }

    public void UpdateSettings(IReadOnlyDictionary<string, object> configuration)
    {
        configuration.ForEach(c => _config[c.Key] = c.Value);
    }

    public void UpdateSetting(string key, object value) => _config[key] = value;

    public void OnLoadOrderHandler(Func<IReadOnlyDictionary<string, object>, AccountBase> loader) => _orderHandlerloader = loader;
    public void OnLoadMarketDataRealtimeSource(Func<IReadOnlyDictionary<string, object>, IRealtimeSource> loader) => _realtimeSourceloader = loader;
    public void OnLoadMarketDataHistoricalSource(Func<IReadOnlyDictionary<string, object>, IHistoricalSource> loader) => _historicalSourceloader = loader;

    #region Application Common APIs
    public IReadOnlyDictionary<string, object> AppSettings => _config;
    public Dictionary<string, IRealtimeSource> RealtimeSources { get; } = new();
    public Dictionary<string, IHistoricalSource> HistoricalSources { get; } = new();

    public async virtual Task InitializeAsync()
    {
        _orderHandlerloader(_config); // Must be first
        _realtimeSourceloader(_config);
        _historicalSourceloader(_config);

        await (Initialized?.Invoke(this) ?? Task.CompletedTask);
    }

    public async virtual Task OpenAsync()
    {
        //_market = await _account.GetMarketAsync((string)_config["MarketCode"]);
        //_config["DefaultOrderSize"] = _market.MinimumOrderSize;

        // ObservableEventPattern converts from EventHandler events to Observable to call user handlers with managing threads 
        Observable.FromEventPattern<OrderEventArgs>(_market, nameof(_market.OrderEvent))
            .ObserveOn(System.Reactive.Concurrency.Scheduler.Default)
            .Subscribe(e =>
            {
                // ToDo: Log order info
                OrderTransactionChanged?.Invoke(e.Sender, e.EventArgs);
            });
        Observable.FromEventPattern<OrderPositionEventArgs>(_market, nameof(_market.PositionChanged))
            .ObserveOn(System.Reactive.Concurrency.Scheduler.Default)
            .Subscribe(e =>
            {
                // ToDo: Log position info
                PositionChanged?.Invoke(e.Sender, e.EventArgs); // PositionChanged event will disconnect WebSocket if dispatch directly
            });

        foreach (var rs in RealtimeSources.Values)
        {
            await rs.OpenAsync();
        }
        //HistoricalSources.ForEach(async e => await e.OpenAsync());

        _market.Open();
        await (Opened?.Invoke(this) ?? Task.CompletedTask);
    }
    #endregion Application Common APIs

    #region Order APIs
    public event EventHandler<OrderEventArgs> OrderTransactionChanged;
    public event EventHandler<OrderPositionEventArgs> PositionChanged;

    public async Task LoginAsync(string apiKey, string apiSecret)
    {
        _config["ApiKey"] = apiKey;
        _config["ApiSecret"] = apiSecret;
    }

    public Task CancelTransactionAsync(Ulid txId) => Task.Run(_transactions[txId].Cancel);

    public Task CancelAllTransactionsAsync()
    {
        return Task.Run(() =>
        {
            _transactions.Values.Where(e => e.IsCancelable).OrderBy(e => e.OpenTime).ForEach(tx =>
            {
                tx.Cancel();
            });
        });
    }
    #endregion Order APIs

    #region Market Data APIs
    (string provider, string symbol) SplitPath(string path)
    {
        var p = path.Split('/');
        if (p[0] == "cryptowatch")
        {
            return (p[0], $"{p[1]}/{p[2]}");
        }
        return (p[0], p[1]);
    }
    #endregion Market Data APIs
}
