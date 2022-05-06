//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Financier.Trading
{
    public class OrderStatus
    {
        public DateTime OpenTime { get; private set; }
        public DateTime CloseTime { get; private set; }
        public DateTime Expiration { get; private set; }

        public OrderState OrderState { get; private set; }

        public bool IsCancelable { get; private set; }

        public void ChangeState(OrderState state)
        {
            throw new NotImplementedException();
        }
    }
}
