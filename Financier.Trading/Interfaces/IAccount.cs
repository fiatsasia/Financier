//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financier.Trading
{
    public interface IAccount : IDisposable
    {
        Task OpenAsync();

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
