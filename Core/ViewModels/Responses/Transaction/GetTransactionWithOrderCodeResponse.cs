using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.Transaction
{
    public class GetTransactionWithOrderCodeResponse
    {
        [JsonProperty("PaymentStatus")]
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public decimal AlreadyPaid { get; set; }
        public decimal Unpaid { get; set; }
    }
}
