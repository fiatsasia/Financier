//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;

namespace Financier.Trading
{
    public interface IMarketDataApi
    {
        // Realtime APIs
        event EventHandler<IExecution> ExecutionReceived;
        event EventHandler<ITicker> TickerReceived;
        event EventHandler<IOrderBook> OrderBookReceived;
        event EventHandler<IOhlcvv> OhlcReceived;

        void Subscribe(string path);
        void Unsubscribe(string path);

        // Historical APIs
        IOhlcvv[] GetOhlc(string path, DateTime start, DateTime end);
    }
}
