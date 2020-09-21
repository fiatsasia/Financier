//==============================================================================
// Copyright (c) 2012-2020 Fiats Inc. All rights reserved.
// https://www.fiats.asia/
//

using System;
using System.Collections.Generic;

namespace Financier.Trading
{
    public interface IAccount : IDisposable
    {
        void Open();

        IMarket GetMarket(string marketSymbol);

        //===============================
        decimal UnrealizedProfit { get; }
        decimal RealizedProfit { get; }
        bool HasOpenPosition(IMarket market);
    }

    public interface IAccountCollection : ICollection<IAccount>
    {
    }
}
