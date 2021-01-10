//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Financier.Trading
{
    public interface IApplication : IDisposable
    {
        ValueTask DisposeAsync();

        IReadOnlyDictionary<string, object> AppSettings { get; }
        Task InitializeAsync();
        Task OpenAsync();

        event EventHandler<IOrderTransactionEventArgs> OrderTransactionChanged;
        event EventHandler<IPositionEventArgs> PositionChanged;

        Task LoginAsync(string key, string secret);
        Task<Ulid> PlaceOrderAsync(IOrderRequest request);
        Task CancelTransactionAsync(Ulid transactionId);
        Task CloseAllPositionsAsync();
    }
}
