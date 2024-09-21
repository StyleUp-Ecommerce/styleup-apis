using CleanBase.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class CartItem : EntityNameAuditActive
    {
        public int Quantity { get; set; }

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
