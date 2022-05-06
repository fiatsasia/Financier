//==============================================================================
// Copyright (c) 2012-2022 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;

namespace Financier.Trading
{
    public class OrderPositionEventArgs : EventArgs
    {
        public PositionEventType EventType { get; set; }

        public OrderPosition Position { get; set; }

        public OrderPositionEventArgs()
        {
        }

        public OrderPositionEventArgs(PositionEventType eventType, OrderPosition position)
        {
            EventType = eventType;
            Position = position;
        }
    }
}
