//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading;

public interface ITradingApp : IDisposable
{
    IReadOnlyDictionary<string, object> AppSettings { get; }

    event Func<ITradingApp, Task> Opened;
    event EventHandler<OrderEventArgs> OrderTransactionChanged;
    event EventHandler<OrderPositionEventArgs> PositionChanged;

    Task InitializeAsync();
    Task OpenAsync();
    ValueTask DisposeAsync();
}
