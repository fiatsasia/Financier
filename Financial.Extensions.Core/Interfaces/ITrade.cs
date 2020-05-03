//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions.Trading
{
    public interface ITrade
    {
        DateTime OpenTime { get; }
        DateTime CloseTime { get; }

        PositionState Status { get; }
        TradeSide Side { get; }

        bool IsOpened { get; }
        bool IsClosed { get; }

        decimal UnrealizedProfit { get; }
        decimal RealizedProfit { get; }
    }

    public interface ITrade<TPrice, TSize> : ITrade
    {
        TPrice OpenPrice { get; }
        TPrice ClosePrice { get; }
        TSize Size { get; }

        void Open(DateTime time, TPrice openPrice, TSize size);
        void Close(DateTime time, TPrice closePrice);
    }

    // 取引管理機能
    // - 執行情報を元に、ポジションの open / close を行う。
    // - 証拠金取引、差金決済取引(margin position) を管理する。
    public interface ITrades
    {
    }

    // 差金決済取引
    // - 注文の執行で、ポジション(margin position)を生成する。
    // - ポジション決済
    public interface IMarginTrade
    {

    }
}
