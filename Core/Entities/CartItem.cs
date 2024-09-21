using CleanBase.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class CartItem : EntityNameAuditActive
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "Size cannot exceed 5 characters.")]
        public string Size { get; set; }

        [Required]
        public Guid CartId { get; set; }

        [Required]
        public Guid CustomCanvasId { get; set; }

        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }
        [ForeignKey("CustomCanvasId")]
        public virtual CustomCanvas CustomCanvas { get; set; }

    }
}
