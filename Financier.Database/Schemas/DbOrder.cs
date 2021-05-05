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
    public class DbOrder
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Required]
        [Column(Order = 1)]
        public string OrderType { get; set; }

        [Required]
        [Column(Order = 2)]
        public string Status { get; set; }

        [Column(Order = 3)]
        public decimal? Size { get; set; }

        [Column(Order = 4)]
        public decimal? Price1 { get; set; }

        [Column(Order = 5)]
        public decimal? Price2 { get; set; }

        [Column(Order = 6)]
        public DateTime? OpenTime { get; set; }

        [Column(Order = 7)]
        public DateTime? CloseTime { get; set; }

        [Column(Order = 8)]
        public Guid? ParentId { get; set; }

        [Required]
        [Column(Order = 9)]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Column(Order = 10)]
        public string Metadata { get; set; }

        public DbOrder() { }

        public DbOrder(IOrderEntity entity)
        {
            Id = new Guid(entity.Id.ToByteArray());
            OrderType = entity.OrderType.ToString();
            Size = entity.Size;
            Price1 = entity.Price1;
            Price2 = entity.Price2;
            ParentId = entity.ParentId.HasValue ? new Guid(entity.ParentId.Value.ToByteArray()) : default;

            Update(entity);
        }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public void Update(IOrderEntity entity)
        {
            Status = entity.Status.ToString();
            OpenTime = entity.OpenTime;
            CloseTime = entity.CloseTime;
            ExpirationDate = entity.ExpirationDate;
            Metadata = entity.Metadata;
        }
    }
}