//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
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
        event EventHandler<IOrderTransactionEventArgs> OrderTransactionChanged;
        event EventHandler<IPositionEventArgs> PositionChanged;

        Task LoginAsync(string key, string secret);
        Task PlaceOrderAsync(IOrderRequest request);
        Task CancelTransactionAsync(Ulid txId);
        Task CancelAllTransactionsAsync();
        Task CloseAllPositionsAsync();
    }
}
