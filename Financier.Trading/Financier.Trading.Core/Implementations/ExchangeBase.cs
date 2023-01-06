//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading;

public abstract class ExchangeBase : IDisposable
{
    public virtual void Dispose() { }
    public virtual Task<MarketBase> GetMarketAsync(string marketCode) => throw new NotSupportedException();
    public virtual Task<AccountBase> GetAccountAsync() => throw new NotSupportedException();
}
