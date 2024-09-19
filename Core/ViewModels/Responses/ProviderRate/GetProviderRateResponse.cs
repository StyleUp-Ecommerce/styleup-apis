﻿using CleanBase.Core.ViewModels.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.ProviderRate
{
    public class GetProviderRateResponse : EntityAuditResponseBase
    {
        public int Start { get; set; }
        public string? Message { get; set; }
        public string AuthorName { get; set; }
    }
}
