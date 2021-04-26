//==============================================================================
// Copyright (c) 2012-2021 Fiats Inc. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt in the solution folder for
// full license information.
// https://www.fiats.asia/
// Fiats Inc. Nakano, Tokyo, Japan
//

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Financier.Trading;

namespace Financier.Database
{
    public class DbExecution
    {
        [Required]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Required]
        [Column(Order = 1)]
        public int Serial { get; set; }

        [Required]
        [Column(Order = 2)]
        public DateTime Time { get; set; }

        [Required]
        [Column(Order = 3)]
        public decimal Size { get; set; }

        [Required]
        [Column(Order = 4)]
        public decimal Price { get; set; }

        [Required]
        [Column(Order = 5)]
        public decimal Commission { get; set; }

        [Required]
        [Column(Order = 6)]
        public string Metadata { get; set; }

        public DbExecution() { }

        public DbExecution(IExecutionEntity entity)
        {
            Id = new Guid(entity.Id.ToByteArray());
            Serial = entity.Index;
            Time = entity.Time;
            Size = entity.Size;
            Price = entity.Price;
            Commission = entity.Commission;
            Metadata = entity.Metadata;
        }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbExecution>().HasKey(b => new { b.Id, b.Serial });
        }
    }
}
