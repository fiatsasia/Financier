//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading;

public interface ITradeLogger : IDisposable
{
    Task OpenAsync();
    Task CloseAsync();

    Task LogAsync(OrderEventArgs args);
    Task LogAsync(OrderPositionEventArgs args);
    Task LogAsync(OrderTransactionBase tx);
    Task LogAsync(Order order);
    Task LogAsync(OrderExecution exec);
    Task LogAsync(OrderPosition pos);
}

public interface ITradeLoggerCollection : ICollection<ITradeLogger>, ITradeLogger
{
}
