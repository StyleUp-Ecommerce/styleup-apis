using CleanBase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.Provider
{
    public class ProviderReqest : EntityNameAuditActive
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal MinimumPrice { get; set; }

        public string Colors { get; set; }
        public string Sizes { get; set; }
        public string Strengths { get; set; }
    }
}
