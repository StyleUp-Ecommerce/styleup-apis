using CleanBase.Core.Entities;
using Core.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Order : EntityNameAuditActive
    {
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(15)]
        public string RecipientPhone { get; set; }

        [Required]
        public string OrderCode { get; set; }

        public string? VoucherCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string RecipientName { get; set; }

        [NotMapped]
        public StatusEnum Status { get; set; } = StatusEnum.Pending;

        public Guid? AuthorId { get; set; }

        [Required]
        [MaxLength(20)]
        public string StatusString
        {
            get => Status.ToString();
            set => Status = Enum.TryParse(value, out StatusEnum status) ? status : default;
        }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual User? User { get; set; }
    }
}
