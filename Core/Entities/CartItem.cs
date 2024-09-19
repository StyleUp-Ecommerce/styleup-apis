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
    public class CartItem : EntityAuditActive
    {

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public Guid CartId { get; set; }

        [ForeignKey("CartId")]
        public Cart Cart { get; set; }
        public Guid CustomCanvasId { get; set; }

        [ForeignKey("CustomCanvasId")]
        public CustomCanvas CustomCanvas { get; set; }

    }
}
