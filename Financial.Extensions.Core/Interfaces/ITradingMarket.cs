//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Extensions
{
    public interface ITradingMarket<TPrice, TSize>
    {
        TradingOrderFactoryBase GetTradeOrderFactory();

        Task PlaceOrder(ITradingOrder order);

        event Action<ITradingOrder> OrderChanged;
        event Action<ITradingPosition<TPrice, TSize>> PositionChanged;

        TPrice BestBidPrice { get; }
        TSize BestBidSize { get; }
        TPrice BestAskPrice { get; }
        TSize BestAskSize { get; }

#if false
        string MarketSymbol { get; }

        IEnumerable<IFxTradingOrder> ListOrders();
        IEnumerable<IFxTradingPosition> ListPositions();


#endif
    }
}
