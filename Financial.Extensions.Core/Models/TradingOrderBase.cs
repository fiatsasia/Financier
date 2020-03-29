//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Financial.Extensions
{
    public abstract class TradingOrderBase : ITradingOrder
    {
        public DateTime OpenTime { get; protected set; }
        public DateTime CloseTime { get; protected set; }
        public TradingOrderState Status { get; protected set; }
    }

    public abstract class TradingMarketPriceOrderBase<TAmount, TSize> : TradingOrderBase
    {
        public TSize Size { get; protected set; }
        public IEnumerable<ITradingExecution<TAmount, TSize>> Executions { get; }
    }

    public abstract class TradingLimitPriceOrderBase<TAmount, TSize> : TradingOrderBase where TAmount : IComparable
    {
        public TAmount Price { get; protected set; }
        public TSize Size { get; protected set; }

        public abstract IEnumerable<ITradingExecution<TAmount, TSize>> Executions { get; }

        public virtual TAmount ExecutedPrice
        {
            get
            {
                if (typeof(TAmount) == typeof(double))
                {
                    return (TAmount)(object)Executions.Average(e => (double)(object)e.Price);
                }
                else if (typeof(TAmount) == typeof(decimal))
                {
                    return (TAmount)(object)Executions.Average(e => (decimal)(object)e.Price);
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        public virtual TSize ExecutedSize
        {
            get
            {
                if (typeof(TSize) == typeof(double))
                {
                    return (TSize)(object)Executions.Sum(e => (double)(object)e.Size);
                }
                else if (typeof(TSize) == typeof(decimal))
                {
                    return (TSize)(object)Executions.Sum(e => (decimal)(object)e.Size);
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }

        public virtual void Executed(TAmount executedPricce, TSize executedSize)
        {

        }
    }
}
