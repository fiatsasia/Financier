//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Financier.Trading
{
    public class LoggerCollection : Collection<ITradeLogger>, ITradeLoggerCollection
    {
        public async Task OpenAsync() => await Task.WhenAll(this.Select(e => e.OpenAsync()));
        public async Task CloseAsync() => await Task.WhenAll(this.Select(e => e.CloseAsync()));
        public async Task LogAsync(IOrderTransactionEventArgs args) => await Task.WhenAll(this.Select(e => e.LogAsync(args)));
        public async Task LogAsync(IPositionEventArgs args) => await Task.WhenAll(this.Select(e => e.LogAsync(args)));
        public async Task LogAsync(IOrder order) => await Task.WhenAll(this.Select(e => e.LogAsync(order)));
        public async Task LogAsync(IOrderExecution execution) => await Task.WhenAll(this.Select(e => e.LogAsync(execution)));
        public async Task LogAsync(IPosition position) => await Task.WhenAll(this.Select(e => e.LogAsync(position)));
    }
}
