using CleanBase.Core.ViewModels.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.SuggestionCanvas
{
    public class SuggestionCanvasRequest : EntityRequestBase
    {
        public string? Content { get; set; }

        public string? Images { get; set; }
    }
}
