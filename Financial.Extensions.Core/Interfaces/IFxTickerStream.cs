using System;
using System.Collections.Generic;
using System.Text;

namespace Financial.Extensions
{
    public interface IFxTickerStream
    {
        DateTime Time { get; }
        decimal BestBidPrice { get; }
        decimal BestBidSize { get; }
        decimal BestAskPrice { get; }
        decimal BestAskSize { get; }
    }
}
