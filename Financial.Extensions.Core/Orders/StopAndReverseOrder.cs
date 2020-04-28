//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financial.Extensions
{
    public class StopAndReverseOrder<TPrice, TSize> : TradingOrder<TPrice, TSize>
    {
        public override bool HasChildOrder => true;

        ITradingOrder<TPrice, TSize> _settleOrder;
        ITradingOrder<TPrice, TSize> _reverseOrder;

        List<ITradingOrder<TPrice, TSize>> _children;
        public override IReadOnlyList<ITradingOrder<TPrice, TSize>> Children => _children;

        public StopAndReverseOrder(ITradingPosition<TPrice, TSize> position)
        {
            Position = position;
            _settleOrder = new TradingMarketPriceOrder<TPrice, TSize>(Calculator.Invert(position.Size), position, this);
            _reverseOrder = new TradingMarketPriceOrder<TPrice, TSize>(Calculator.Invert(position.Size), this);
            _children = new List<ITradingOrder<TPrice, TSize>>
            {
                _settleOrder,
                _reverseOrder
            };
        }

        public override bool CanExecute(TPrice price)
        {
            return true;
        }

        public override void ExecutePartial(DateTime time, TPrice executePrice, TSize executeSize)
        {
            if (_settleOrder != null)
            {
                _settleOrder.ExecutePartial(time, executePrice, executeSize);
                _reverseOrder = _settleOrder;
                _settleOrder = null;
            }
            else
            {
                var reverseOrder = new TradingMarketPriceOrder<TPrice, TSize>(Calculator.Invert(_settleOrder.OrderSize));
            }
        }
    }
}
