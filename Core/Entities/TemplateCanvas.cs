using CleanBase.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class TemplateCanvas : EntityNameAuditActive
    {
        [MaxLength(1000, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Descriptions { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Image path cannot exceed 250 characters.")]
        public string Image { get; set; }

        [Column(TypeName = "jsonb")]
        public string Content { get; set; }

        [Required]
        public Guid ProviderId { get; set; }

        [ForeignKey(nameof(ProviderId))]
        public virtual Provider Provider { get; set; }
        public virtual ICollection<CustomCanvas> CustomCanvas { get; set; }
    }
}
