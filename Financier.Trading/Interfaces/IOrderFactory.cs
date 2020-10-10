//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

namespace Financier.Trading
{
    public interface IOrderFactory
    {
        // Basic orders (mandatory)
        IOrder MarketPrice(decimal size);
        IOrder LimitPrice(decimal price, decimal size);

        // Simple conditional orders
        IOrder StopLoss(decimal triggerPrice, decimal size);
        IOrder StopLimit(decimal triggerPrice, decimal stopPrice, decimal size);
        IOrder TrailingStop(decimal trailingOffset, decimal size);
        IOrder TrailingStopLimit(decimal trailingOffset, decimal stopPrice, decimal size);
        IOrder TakeProfit(decimal profitPrice, decimal size);
        IOrder TakeProfitLimit(decimal profitPrice, decimal limitPrice, decimal size);

        // Combined conditional orders
        IOrder IFD(IOrder ifOrder, IOrder doneOrder);
        IOrder OCO(IOrder first, IOrder second);
        IOrder IFDOCO(IOrder ifOrder, IOrder first, IOrder second);
    }
}
