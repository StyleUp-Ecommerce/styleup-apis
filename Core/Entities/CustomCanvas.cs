using CleanBase.Core.Entities;
using Core.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class CustomCanvas : EntityNameAuditActive
    {
        [Column(TypeName = "jsonb")]
        public string Content { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string? LensVRUrl { get; set; }
        public bool IsPublic { get; set; } = false;

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [NotMapped]
        public ColorEnum Color { get; set; }
        [Required]
        [MaxLength(20)]
        public string ColorString
        {
            get => Color.ToString();
            set => Color = Enum.TryParse(value, out ColorEnum color) ? color : default;
        }

        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public Guid TemplateId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }

        [ForeignKey(nameof(TemplateId))]
        public virtual TemplateCanvas Template { get; set; }
    }
}
