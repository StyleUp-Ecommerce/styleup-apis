using CleanBase.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Cart : EntityNameAuditActive
    {
        [Required]
        public Guid AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }
        public virtual ICollection<CartItem>? CartItems { get; set; }
    }

}
