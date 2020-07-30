//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;

namespace Financier.Trading
{
    public abstract class OrderFactoryBase : IOrderFactory
    {
        public virtual IOrder CreateMarketPriceOrder(decimal size) => throw new NotSupportedException();
        public virtual IOrder CreateLimitPriceOrder(decimal price, decimal size) => throw new NotSupportedException();
        public virtual IOrder CreateStopOrder(decimal stopPrice, decimal size) => throw new NotSupportedException();
        public virtual IOrder CreateStopLimitOrder(decimal stopPrice, decimal orderPrice, decimal size) => throw new NotSupportedException();

        public virtual IOrder CreateTrailingStopOrder(decimal trailingStopPriceOffset, decimal size) => throw new NotSupportedException();
        public virtual IOrder CreateStopAndReverseOrder(decimal stopPrice, decimal size) => throw new NotSupportedException();

        public virtual IOrder CreateIFD(IOrder first, IOrder second) => throw new NotSupportedException();
        public virtual IOrder CreateOCO(IOrder first, IOrder second) => throw new NotSupportedException();
        public virtual IOrder CreateIFDOCO(IOrder ifdone, IOrder ocoFirst, IOrder ocoSecond) => throw new NotSupportedException();
    }
}
