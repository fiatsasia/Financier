﻿//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public interface ITrade
    {
        DateTime OpenTime { get; }
        DateTime CloseTime { get; }

        PositionState Status { get; }

        bool IsOpened { get; }
        bool IsClosed { get; }

        decimal UnrealizedProfit { get; }
        decimal RealizedProfit { get; }

        decimal OpenPrice { get; }
        decimal ClosePrice { get; }
        decimal TradeSize { get; }

        void Open(DateTime time, decimal openPrice, decimal size);
        void Close(DateTime time, decimal closePrice);
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