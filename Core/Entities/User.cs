using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }
        
        [MaxLength(250, ErrorMessage = "Avatar URL cannot exceed 250 characters.")]
        public string? AvatarUrl { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<ProviderRate>? ProviderRates { get; set; }
        public virtual ICollection<UserVoucher>? UserVouchers { get; set; }
    }
}
