//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
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
    public interface ITradeLogger : IDisposable
    {
        Task OpenAsync();
        Task CloseAsync();

        Task LogAsync(IOrderTransactionEventArgs args);
        Task LogAsync(IPositionEventArgs args);
        Task LogAsync(IOrderEntity order);
        Task LogAsync(IExecutionEntity exec);
        Task LogAsync(IPositionEntity pos);
    }

    public interface ITradeLoggerCollection : ICollection<ITradeLogger>, ITradeLogger
    {
    }
}
