//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Threading.Tasks;

namespace Financier.Trading
{
    // - MarketConfigを追加
    //      - 両建て不可
    //          - 反対売買は建玉相殺
    //      - 売り建て不可
    //      - ポジション合算
    public interface IMarketConfig
    {
    }

    public interface IMarket
    {
        DateTime LastUpdatedTime { get; }

        bool HasActiveOrder { get; }

        // Precision (price and size)
        // Number of decimal places (price and size)
        decimal MarketPrice { get; }

        IOrderFactory OrderFactory { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Order dispatched immediately</returns>
        bool PlaceOrder(IOrder order);

        Task PlaceOrderAsync(IOrder order);

        bool PlaceOrder(IOrder order, TimeInForce tif);

        void UpdatePrice(DateTime time, decimal price);

        event Action<IOrder> OrderChanged;
    }
}
