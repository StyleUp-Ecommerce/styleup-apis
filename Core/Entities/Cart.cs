﻿using CleanBase.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Cart : EntityNameAuditActive
    {
        public virtual ICollection<CartItem>? CartItems { get; set; }
    }

}
