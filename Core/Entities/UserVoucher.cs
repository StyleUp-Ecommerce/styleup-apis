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
    public class UserVoucher : EntityAuditActive
    {
        [Key, ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Key, ForeignKey(nameof(VoucherId))]
        public Voucher Voucher {  get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid VoucherId { get; set; }

        [Required]
        public decimal OriginBill { get; set; }


        [Required]
        public decimal DiscountBill { get; set; }
    }
}
