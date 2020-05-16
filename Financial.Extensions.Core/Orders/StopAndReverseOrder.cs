//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financial.Extensions.Trading
{
    public class StopAndReverseOrder<TPrice, TSize> : Order<TPrice, TSize>
    {
        IOrder<TPrice, TSize> _stopOrder;
        IOrder<TPrice, TSize> _reverseOrder;

        IOrder<TPrice, TSize>[] _children;
        public override IReadOnlyList<IOrder<TPrice, TSize>> Children => _children;

        public StopAndReverseOrder(TPrice stopPrice, TSize size)
        {
            OrderType = OrderType.StopAndReverse;
            OrderSize = size;

            _stopOrder = new StopOrder<TPrice, TSize>(stopPrice, size);
            _reverseOrder = new MarketPriceOrder<TPrice, TSize>(Calculator.Invert(size));
            _children = new IOrder<TPrice, TSize>[] { _stopOrder, _reverseOrder };
        }

        public override void Open(DateTime time)
        {
            base.Open(time);
            _stopOrder.Open(time);
        }

        public override bool TryExecute(DateTime time, TPrice executePrice)
        {
            if (!_stopOrder.TryExecute(time, executePrice))
            {
                return false;
            }

            _reverseOrder.Open(time);
            _reverseOrder.TryExecute(time, executePrice);
            CloseTime = time;

            return true;
        }
    }
}
