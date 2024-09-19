﻿using CleanBase.Core.Entities;
using Core.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Order : EntityAuditActive
    {
        public string Address { get; set; }

        [NotMapped]
        public StatusEnum Status { get; set; }

        [Required]
        [MaxLength(20)]
        public string StatusString
        {
            get => Status.ToString();
            set => Status = Enum.TryParse(value, out StatusEnum status) ? status : default;
        }

        [Required]
        public Guid CartId { get; set; }
        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; }
    }
}
