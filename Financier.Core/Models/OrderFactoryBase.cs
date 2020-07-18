//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public abstract class OrderFactoryBase<TPrice, TSize> : IOrderFactory<TPrice, TSize>
    {
        public virtual IOrder<TPrice, TSize> CreateMarketPriceOrder(TSize size) => throw new NotSupportedException();
        public virtual IOrder<TPrice, TSize> CreateLimitPriceOrder(TPrice price, TSize size) => throw new NotSupportedException();
        public virtual IOrder<TPrice, TSize> CreateStopOrder(TPrice stopPrice, TSize size) => throw new NotSupportedException();
        public virtual IOrder<TPrice, TSize> CreateStopLimitOrder(TPrice stopPrice, TPrice orderPrice, TSize size) => throw new NotSupportedException();

        public virtual IOrder<TPrice, TSize> CreateTrailingStopOrder(TPrice trailingStopPriceOffset, TSize size) => throw new NotSupportedException();
        public virtual IOrder<TPrice, TSize> CreateStopAndReverseOrder(TPrice stopPrice, TSize size) => throw new NotSupportedException();

        public virtual IOrder<TPrice, TSize> CreateIFD(IOrder first, IOrder second) => throw new NotSupportedException();
        public virtual IOrder<TPrice, TSize> CreateOCO(IOrder first, IOrder second) => throw new NotSupportedException();
        public virtual IOrder<TPrice, TSize> CreateIFDOCO(IOrder ifdone, IOrder ocoFirst, IOrder ocoSecond) => throw new NotSupportedException();
    }
}
