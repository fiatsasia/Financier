//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Threading.Tasks;

namespace Financier.Trading
{
    public interface IOrderContext : IDisposable
    {
        Task UpdateAsync(IOrderEntity entity);
        Task AddAsync(IExecutionEntity entity);
        Task UpdateAsync(IPositionEntity entity);
    }
}
