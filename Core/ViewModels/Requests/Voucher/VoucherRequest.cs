using CleanBase.Core.ViewModels.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.Voucher
{
    public class VoucherRequest : EntityRequestBase
    {
        public string Code { get; set; }

        public string DiscountType { get; set; }

        public decimal DiscountValue { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
