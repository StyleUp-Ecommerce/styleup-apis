using CleanBase.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SuggestionCanvas : EntityNameAuditActive
    {
        [Key]
        public  Guid? Id { get; set; }
        [Column(TypeName = "jsonb")]
        public string? Content { get; set; }

        [Column(TypeName = "text")]
        public string? Images { get; set; }

    }
}
