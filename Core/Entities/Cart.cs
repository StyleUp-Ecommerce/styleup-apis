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
    public class Cart : EntityAuditActive
    {
        public decimal TotalAmount { get; set; }


        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual Order Order { get; set; }
    }

}
