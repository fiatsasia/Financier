//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System.Linq;
using System.Collections.ObjectModel;

namespace Financier
{
    public class HistoricalSourceCollection : Collection<IHistoricalSource>, IHistoricalSourceCollection
    {
        public IHistoricalSource Get(string provider) => this.FirstOrDefault(e => e.Provider.ToUpper() == provider.ToUpper());
    }
}
