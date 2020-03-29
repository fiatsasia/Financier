//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions
{
    public interface ITradingPosition
    {
        DateTime OpenTime { get; }
        DateTime CloseTime { get; }
        double ProfitRate { get; }

        TradePositionState Status { get; }
    }

    public interface ITradingPosition<TAmount, TSize> : ITradingPosition
    {
        TAmount OpenPrice { get; }
        TAmount ClosePrice { get; }
        TSize Size { get; }

        TAmount ProfitAmount { get; }

        void Open(DateTime time, TAmount openPrice, TSize size);
        void Close(DateTime time, TAmount closePrice);
        TAmount CalculateProfit(TAmount currentPrice);
        double CalculateProfitRate(TAmount currentPrice);
    }
}
