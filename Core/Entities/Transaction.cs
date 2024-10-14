using CleanBase.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Transaction : EntityNameAuditActive
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public string TransactionCode {  get; set; }
        public string PaymentMethod {  get; set; }
        public string? PaymentUrl { get; set; } 
        public string ReturnUrl { get; set; }
        public decimal AlreadyPaid { get; set; }
        public decimal Unpaid { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
    }
}
