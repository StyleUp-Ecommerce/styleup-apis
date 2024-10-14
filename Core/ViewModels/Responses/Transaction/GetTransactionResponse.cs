using CleanBase.Core.ViewModels.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.Transaction
{
    public class GetTransactionResponse : EntityResponseBase
    {
        public long TransactionId { get; set; }
        public string OrderInfo { get; set; }
        public string Amount { get; set; }
        public string BankCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public string PaymentUrl { get; set; }
        public string ReturnUrl { get; set; }
    }
}
