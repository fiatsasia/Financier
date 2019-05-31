using System;
using System.Collections.Generic;
using System.Text;

namespace Financial.Extensions
{
    public interface IFxTradeStream
    {
        DateTime Time { get; }
        decimal Price { get; }
        decimal Size { get; }       // +buy -sell
    }
}
