using CleanBase.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class OrderItem : EntityNameAuditActive
    {
        public int Quantity { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public Guid CustomCanvasId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [ForeignKey("CustomCanvasId")]
        public virtual CustomCanvas CustomCanvas { get; set; }
    }
}
