//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Collections.Generic;
using System.Text;
using Financier.Trading;

namespace Financier.Database
{
    public interface IOrderDbContext
    {
        void Update(IOrderEntity entity);
        void Add(IExecutionEntity entity);
    }
}
