using CleanBase.Core.ViewModels.Response.Base;

namespace Core.ViewModels.Responses.ProviderRate
{
    public class ProviderRateResponse : EntityAuditResponseBase
    {
        public int Start { get; set; }
        public string? Message { get; set; }
        public UserRateResponse User { get; set; }
    }
}
