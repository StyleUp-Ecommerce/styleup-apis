using Core.ViewModels.Responses.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.Order
{
    public class GetOrderByCodeResponse
    {
        public OrderResponse Order { get; set; }
        public GetTransactionWithOrderCodeResponse Transaction { get; set; }
    }
}
