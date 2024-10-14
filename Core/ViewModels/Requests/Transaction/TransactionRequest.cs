using CleanBase.Core.ViewModels.Request.Base;
using Core.ViewModels.Requests.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.Transaction
{
    public class TransactionRequest : EntityRequestBase
    {
        public OrderRequest Order { get; set; }
        public string PaymentMethod { get; set; }
        public string ReturnUrl { get; set; }
    }
}
