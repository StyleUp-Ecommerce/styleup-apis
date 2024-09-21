using CleanBase.Core.ViewModels.Response.Base;

namespace Core.ViewModels.Responses.ProviderRate
{
    public class GetProviderRateResponse : EntityAuditResponseBase
    {
        public int Start { get; set; }
        public string? Message { get; set; }
        public string AuthorName { get; set; }
    }
}
