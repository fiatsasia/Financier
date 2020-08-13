//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public abstract class OrderFactoryBase : IOrderFactory
    {
        public virtual IOrder MarketPrice(decimal size) => throw new NotSupportedException();
        public virtual IOrder LimitPrice(decimal price, decimal size) => throw new NotSupportedException();
        public virtual IOrder StopLoss(decimal stopPrice, decimal size) => throw new NotSupportedException();
        public virtual IOrder StopLimit(decimal stopPrice, decimal orderPrice, decimal size) => throw new NotSupportedException();

        public virtual IOrder TrailingStop(decimal trailingStopPriceOffset, decimal size) => throw new NotSupportedException();
        public virtual IOrder StopAndReverse(decimal stopPrice, decimal size) => throw new NotSupportedException();

        public virtual IOrder IFD(IOrder first, IOrder second) => throw new NotSupportedException();
        public virtual IOrder OCO(IOrder first, IOrder second) => throw new NotSupportedException();
        public virtual IOrder IFDOCO(IOrder ifdone, IOrder ocoFirst, IOrder ocoSecond) => throw new NotSupportedException();
    }
}
