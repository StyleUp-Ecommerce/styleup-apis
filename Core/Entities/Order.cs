using CleanBase.Core.Entities;
using Core.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Order : EntityNameAuditActive
    {
        public string Address { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientName { get; set; }
        [NotMapped]
        public StatusEnum Status { get; set; } = StatusEnum.Pending;

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [MaxLength(20)]
        public string StatusString
        {
            get => Status.ToString();
            set => Status = Enum.TryParse(value, out StatusEnum status) ? status : default;
        }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual User User { get; set; }
    }
}
