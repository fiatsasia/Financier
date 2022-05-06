//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financier.Trading
{
    public interface ITradingApp : IDisposable
    {
        IReadOnlyDictionary<string, object> AppSettings { get; }

        event Func<ITradingApp, Task> Opened;
        event EventHandler<OrderTransactionEventArgs> OrderTransactionChanged;
        event EventHandler<OrderPositionEventArgs> PositionChanged;

        Task InitializeAsync();
        Task OpenAsync();
        ValueTask DisposeAsync();

        IMarketDataSources GetMarketDataSources();
    }
}
