using CleanBase.Core.ViewModels.Response.Base;

namespace Core.ViewModels.Responses.Provider
{
    public class ProviderResponse : EntityAuditNameResponseBase
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal MinimumPrice { get; set; }

        public string Colors { get; set; }
        public string Sizes { get; set; }
        public string Strengths { get; set; }
    }
}
