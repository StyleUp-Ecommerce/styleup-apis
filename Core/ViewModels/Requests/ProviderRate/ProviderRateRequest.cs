using CleanBase.Core.ViewModels.Request.Base;
using CleanBase.Core.ViewModels.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.ProviderRate
{
    public class ProviderRateRequest : EntityRequestBase
    {
        public int Start { get; set; }
        public string? Message { get; set; }

        public Guid AuthorId { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProviderId { get; set; }
    }
}
