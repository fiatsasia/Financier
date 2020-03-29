using System;
using System.Collections.Generic;
using System.Text;

namespace Financial.Extensions
{
    public enum TradePositionState
    {
        Active,
        Closed,
    }

    public enum TradeTimeInForce
    {
        GoodTilCanceled,
        FillOrKill,
    }

}
