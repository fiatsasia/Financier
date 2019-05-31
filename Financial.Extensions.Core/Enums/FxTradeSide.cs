using System;
using System.Collections.Generic;
using System.Text;

namespace Financial.Extensions
{
    public enum FxTradeSide
    {
        Unknown,
        Buy,
        Sell,
    }

    public static class FxTradeSideExtension
    {
        public static FxTradeSide Opposite(this FxTradeSide side)
        {
            switch (side)
            {
                case FxTradeSide.Buy:
                    return FxTradeSide.Sell;

                case FxTradeSide.Sell:
                    return FxTradeSide.Buy;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
