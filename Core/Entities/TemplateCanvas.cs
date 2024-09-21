using CleanBase.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class TemplateCanvas : EntityNameAuditActive
    {
        public string Descriptions { get; set; }
        public string Image { get; set; }

        [Column(TypeName = "jsonb")]
        public string Content { get; set; }

        [Required]
        public Guid ProviderId { get; set; }

        [ForeignKey(nameof(ProviderId))]
        public virtual Provider Provider { get; set; }
    }
}
