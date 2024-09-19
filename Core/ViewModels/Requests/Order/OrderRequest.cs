using CleanBase.Core.Entities;
using CleanBase.Core.ViewModels.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.Order
{
    public class OrderRequest : EntityRequestBase
    {
        public string Address { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientName { get; set; }
        public Guid CartId { get; set; }
    }
}
