//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System.Linq;
using System.Collections.ObjectModel;

namespace Financier.Trading
{
    public class AccountCollection : Collection<IAccount>, IAccountCollection
    {
        public IAccount Get(string exchange) => this.FirstOrDefault(e => e.Exchange == exchange);
    }
}
