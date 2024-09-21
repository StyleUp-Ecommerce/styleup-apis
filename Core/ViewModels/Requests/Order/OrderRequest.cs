using CleanBase.Core.ViewModels.Request.Base;

namespace Core.ViewModels.Requests.Order
{
    public class OrderRequest : EntityRequestBase
    {
        public string Address { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientName { get; set; }
        public Guid CartId { get; set; }
    }
}
