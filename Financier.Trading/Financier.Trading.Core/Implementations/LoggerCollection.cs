//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading;

public class LoggerCollection : Collection<ITradeLogger>, ITradeLoggerCollection, IDisposable
{
    public void Dispose() => this.ForEach(e => e.Dispose());

    public async Task OpenAsync() => await Task.WhenAll(this.Select(e => e.OpenAsync()));
    public async Task CloseAsync() => await Task.WhenAll(this.Select(e => e.CloseAsync()));
    public async Task LogAsync(OrderEventArgs args) => await Task.WhenAll(this.Select(e => e.LogAsync(args)));
    public async Task LogAsync(OrderPositionEventArgs args) => await Task.WhenAll(this.Select(e => e.LogAsync(args)));
    public async Task LogAsync(OrderTransactionBase tx) => await Task.WhenAll(this.Select(e => e.LogAsync(tx)));
    public async Task LogAsync(Order order) => await Task.WhenAll(this.Select(e => e.LogAsync(order)));
    public async Task LogAsync(OrderExecution exec) => await Task.WhenAll(this.Select(e => e.LogAsync(exec)));
    public async Task LogAsync(OrderPosition pos) => await Task.WhenAll(this.Select(e => e.LogAsync(pos)));
}
