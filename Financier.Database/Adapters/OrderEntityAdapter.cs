//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using Financier.Trading;

namespace Financier.Database
{
    class OrderEntityAdapter : IOrderEntity
    {
        DbOrder _adaptee;

        Ulid _id;
        public Ulid Id
        {
            get
            {
                if (_id == null)
                {
                    _id = new Ulid(_adaptee.Id);
                }
                return _id;
            }
        }

        OrderType _orderType;
        public OrderType OrderType
        {
            get
            {
                if (_orderType == OrderType.Unknown)
                {
                    _orderType = (OrderType)Enum.Parse(typeof(OrderType), _adaptee.OrderType);
                }
                return _orderType;
            }
        }

        OrderState _status;
        public OrderState Status
        {
            get
            {
                if (_status == OrderState.Unknown)
                {
                    _status = (OrderState)Enum.Parse(typeof(OrderState), _adaptee.Status);
                }
                return _status;
            }
        }

        public decimal? Size => _adaptee.Size;
        public decimal? Price1 => _adaptee.Price1;
        public decimal? Price2 => _adaptee.Price2;

        public DateTime? OpenTime => _adaptee.OpenTime;
        public DateTime? CloseTime => _adaptee.CloseTime;

        Ulid? _parentOrderId;
        public Ulid? ParentId
        {
            get
            {
                if (!_parentOrderId.HasValue && _adaptee.ParentId.HasValue)
                {
                    _parentOrderId = new Ulid(_adaptee.ParentId.Value);
                }
                return _parentOrderId;
            }
        }

        public DateTime ExpirationDate => _adaptee.ExpirationDate;

        public string Metadata => _adaptee.Metadata;


        public OrderEntityAdapter(DbOrder adaptee)
        {
            _adaptee = adaptee;
        }
    }
}
