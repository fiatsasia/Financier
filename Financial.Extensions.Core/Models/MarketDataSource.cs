//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.ObjectModel;

namespace Financial.Extensions
{
    public class RealtimeSourceCollection : Collection<IMarketDataSource>, IRealtimeSourceCollection
    {
    }

    public class HistoricalSourceCollection : Collection<IMarketDataSource>, IHistoricalSourceCollection
    {
    }

    public class HistoricalSignalSourceCollection : Collection<IMarketDataSource>, IHistoricalSignalSourceCollection
    {
    }
}
