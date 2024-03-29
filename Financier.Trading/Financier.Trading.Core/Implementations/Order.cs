﻿//==============================================================================
// Copyright (c) 2012-2023 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

namespace Financier.Trading;

public class Order
{
    public Ulid Id { get; }
    public OrderType OrderType { get; }
    public ReadOnlyCollection<Order> Children { get; }

    // Order parameters
    public virtual decimal? OrderPrice { get; set; }
    public PriceType? OrderPriceType { get; set; }
    public decimal? OrderPriceOffset { get; set; }
    public decimal? OrderPriceOffsetRate { get; set; }
    public virtual decimal? OrderSize { get; set; }

    // Trigger parameters
    public virtual decimal? TriggerPrice { get; set; }
    public PriceType? TriggerPriceType { get; set; }
    public decimal? TriggerPriceOffset { get; set; }
    public decimal? TriggerPriceOffsetRate { get; set; }

    // Stop parameters
    public decimal? StopPrice { get; set; }
    public PriceType? StopPriceType { get; set; }
    public decimal? StopPriceOffset { get; set; }
    public decimal? StopPriceOffsetRate { get; set; }

    public virtual decimal? TrailingOffset { get; set; }    // Trailing stop, trailing stop limit
    public decimal? ProfitPrice { get; set; }       // Take profit


    public TimeSpan? TimeToExpire { get; set; }
    public TimeInForce? TimeInForce { get; set; }

    public Order(OrderType orderType)
    {
        Id = Ulid.NewUlid();
        OrderType = orderType;
        Children = new(new Order[0]);
    }

    public Order(OrderType orderType, params Order[] children)
    {
        switch (orderType)
        {
            case OrderType.IFD:
            case OrderType.OCO:
                if (children.Count() != 2 || children.Any(e => e == null))
                {
                    throw new ArgumentException();
                }
                break;

            case OrderType.IFDOCO:
                if (children.Count() != 3 || children.Any(e => e == null))
                {
                    throw new ArgumentException();
                }
                break;

            default:
                throw new ArgumentException();
        }

        Id = Ulid.NewUlid();
        OrderType = orderType;
        Children = new(children.ToList());
    }
}
