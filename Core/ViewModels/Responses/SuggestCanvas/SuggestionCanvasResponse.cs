using CleanBase.Core.ViewModels.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.SuggestionCanvas
{
    public class SuggestionCanvasResponse : EntityAuditNameResponseBase
    {
        public string? Content { get; set; }

        public string? Images { get; set; }
    }
}
