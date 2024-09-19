using CleanBase.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProviderRate : EntityAuditActive
    {
        public int Start { get; set; }
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
