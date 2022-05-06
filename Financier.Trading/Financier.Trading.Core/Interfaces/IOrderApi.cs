//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Threading.Tasks;

namespace Financier.Trading
{
    public interface IOrderApi
    {
        event EventHandler<OrderTransactionEventArgs> OrderTransactionChanged;
        event EventHandler<OrderPositionEventArgs> PositionChanged;

        Task LoginAsync(string key, string secret);
        Task PlaceOrderAsync(OrderRequest request);
        Task CancelTransactionAsync(Ulid txId);
        Task CancelAllTransactionsAsync();
        Task CloseAllPositionsAsync();
    }
}
