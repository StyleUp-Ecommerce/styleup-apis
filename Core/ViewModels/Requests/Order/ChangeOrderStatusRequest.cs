using CleanBase.Core.ViewModels.Request.Base;
using Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.Order
{
    public class ChangeOrderStatusRequest
    {
        public string OrderCode { get; set; }

        public string Status { get; set; }
    }
}
