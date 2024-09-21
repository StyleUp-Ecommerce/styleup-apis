using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? AvatarUrl { get; set; }
        public Guid CartId { get; set; }

        [ForeignKey(nameof(CartId))]
        public virtual Cart? Cart { get; set; } = new Cart();
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<ProviderRate>? ProviderRates { get; set; }
    }
}
