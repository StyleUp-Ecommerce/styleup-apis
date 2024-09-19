using CleanBase.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Pet : EntityNameAuditActive
    {
        public int Age { get; set; }
    }
}
