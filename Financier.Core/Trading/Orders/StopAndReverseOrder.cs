//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financier.Trading
{
    public class StopAndReverseOrder : Order
    {
        IOrder _stopOrder;
        IOrder _reverseOrder;

        IOrder[] _children;
        public override IReadOnlyList<IOrder> Children => _children;

        public StopAndReverseOrder(decimal stopPrice, decimal size)
        {
            OrderType = OrderType.StopAndReverse;
            OrderSize = size;

            _stopOrder = new StopOrder(stopPrice, size);
            _reverseOrder = new MarketPriceOrder(Calculator.Invert(size));
            _children = new IOrder[] { _stopOrder, _reverseOrder };
        }

        public override void Open(DateTime time)
        {
            base.Open(time);
            _stopOrder.Open(time);
        }

        public override bool TryExecute(DateTime time, decimal executePrice)
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
