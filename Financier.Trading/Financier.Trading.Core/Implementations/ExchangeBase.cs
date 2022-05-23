//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
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
    public abstract class ExchangeBase : IDisposable
    {
        public virtual void Dispose() { }
        public virtual Task<MarketBase> GetMarketAsync(string marketCode) => throw new NotSupportedException();
        public virtual Task<AccountBase> GetAccountAsync() => throw new NotSupportedException();
    }
}
