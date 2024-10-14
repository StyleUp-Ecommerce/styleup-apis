using CleanBase.Core.ViewModels.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.Transaction
{
    public class CreateTransactionResponse : EntityAuditResponseBase
    {
        public string ReturnUrl { get; set; }
        public string Message { get; set; }
        public string PaymentMethod { get; set; }
    }
}
