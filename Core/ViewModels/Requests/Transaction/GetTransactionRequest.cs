using CleanBase.Core.ViewModels.Request;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.Transaction
{
    public class GetTransactionRequest
    {
        public Guid OrderItemId { get; set; }
    }
}
