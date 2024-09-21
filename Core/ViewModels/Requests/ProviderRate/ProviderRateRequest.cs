using CleanBase.Core.ViewModels.Request.Base;

namespace Core.ViewModels.Requests.ProviderRate
{
    public class ProviderRateRequest : EntityRequestBase
    {
        public int Start { get; set; }
        public string? Message { get; set; }

        public Guid AuthorId { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProviderId { get; set; }
    }
}
