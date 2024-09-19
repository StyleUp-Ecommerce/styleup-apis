using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid CartId { get; set; }

        [ForeignKey(nameof(CartId))]
        public virtual Cart? Cart { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<ProviderRate>? ProviderRates { get; set; }
    }
}
