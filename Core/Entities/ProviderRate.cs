﻿using CleanBase.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class ProviderRate : EntityNameAuditActive
    {
        [Range(1, 5, ErrorMessage = "Start must be between 1 and 5.")]
        public int Start { get; set; }

        [Column(TypeName = "text")]
        public string? Message { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid ProviderId { get; set; }


        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }

        [ForeignKey(nameof(ProviderId))]
        public virtual Provider Provider { get; set; }
    }
}
