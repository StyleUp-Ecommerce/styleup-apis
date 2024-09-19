﻿using CleanBase.Core.Entities;
using CleanBase.Core.ViewModels.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.Provider
{
    public class ProviderRequest : EntityRequestBase
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