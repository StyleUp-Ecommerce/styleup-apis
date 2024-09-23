using CleanBase.Core.ViewModels.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.Voucher
{
    public class VoucherResponse : EntityResponseBase
    {
        public string Code { get; set; }

        public string DiscountType { get; set; }

        public decimal DiscountValue { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
