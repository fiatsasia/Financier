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
    public class DbPosition
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Required]
        [Column(Order = 1)]
        public Guid OpenExecutionId { get; set; }

        [Required]
        [Column(Order = 2)]
        public int OpenExecutionIndex { get; set; }

        [Column(Order = 3)]
        public Guid? CloseExecutionId { get; set; }

        [Column(Order = 4)]
        public int? CloseExecutionIndex { get; set; }

        [Required]
        [Column(Order = 5)]
        public decimal Size { get; set; }

        [Required]
        [Column(Order = 6)]
        public string Metadata { get; set; }

        public DbPosition() { }

        public DbPosition(IPositionEntity entity)
        {
            Id = new Guid(entity.Id.ToByteArray());
            OpenExecutionId = new Guid(entity.OpenExecutionId.ToByteArray());
            OpenExecutionIndex = entity.OpenExecutionIndex;
            Size = entity.Size;

            Update(entity);
        }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public void Update(IPositionEntity entity)
        {
            if (entity.CloseExecutionId.HasValue)
            {
                CloseExecutionId = new Guid(entity.CloseExecutionId.Value.ToByteArray());
                CloseExecutionIndex = entity.CloseExecutionIndex;
            }
            Metadata = entity.Metadata;
        }
    }
}
