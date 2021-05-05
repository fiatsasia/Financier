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
    class ExecutionEntityAdapter : IExecutionEntity
    {
        DbExecution _adaptee;

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

        public int Index => _adaptee.Index;

        public DateTime Time => _adaptee.Time;

        public decimal Size => _adaptee.Size;

        public decimal Price => _adaptee.Price;

        public decimal Commission => _adaptee.Commission;

        public string Metadata => _adaptee.Metadata;


        public ExecutionEntityAdapter(DbExecution adaptee)
        {
            _adaptee = adaptee;
        }
    }
}
