using CleanBase.Core.Entities;

namespace Core.Entities
{
    public class Cart : EntityNameAuditActive
    {
        public virtual ICollection<CartItem>? CartItems { get; set; }
    }

}
