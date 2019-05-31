﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Financial.Extensions
{
    public interface IFxOhlc
    {
        DateTime Start { get; }
        decimal Open { get; }
        decimal High { get; }
        decimal Low { get; }
        decimal Close { get; }
    }

    public interface IFxOhlcv : IFxOhlc
    {
        double Volume { get; }
    }

    public interface IFxOhlcvv : IFxOhlcv
    {
        double VWAP { get; }
    }
}
