using CleanBase.Core.Data.Repositories;
using CleanBase.Core.Data.Transactions;
using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Entities;
using Core.ViewModels.Responses.Voucher;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Entities
{
    public class Voucher : EntityNameAuditActive
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string DiscountType { get; set; }

        public decimal DiscountValue { get; set; }

        [Required]
        public DateTime ExpirationDate {  get; set; }

        public virtual ICollection<UserVoucher>? UserVouchers { get; set;}

        public static implicit operator Voucher(VoucherResponse v)
        {
            throw new NotImplementedException();
        }
    }
}
