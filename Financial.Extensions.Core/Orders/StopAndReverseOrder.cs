//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financial.Extensions.Trading
{
    public class StopAndReverseOrder<TPrice, TSize> : Order<TPrice, TSize>
    {
        public IOrder<TPrice, TSize> StopOrder { get; private set; }
        public IOrder<TPrice, TSize> ReverseOrder { get; private set; }

        public StopAndReverseOrder(TPrice stopPrice, TSize stopSize)
        {
            OrderSize = stopSize;

            StopOrder = new StopOrder<TPrice, TSize>(stopPrice, stopSize);
            ReverseOrder = new MarketPriceOrder<TPrice, TSize>(Calculator.Invert(stopSize));
        }

        public override void Open(DateTime time)
        {
            base.Open(time);
            StopOrder.Open(time);
        }

        public override bool TryExecute(DateTime time, TPrice executePrice)
        {
            if (!StopOrder.TryExecute(time, executePrice))
            {
                return false;
            }

            ReverseOrder.Open(time);
            ReverseOrder.TryExecute(time, executePrice);
            CloseTime = time;

            return true;
        }
    }
}
