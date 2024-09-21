using CleanBase.Core.ViewModels.Request.Base;

namespace Core.ViewModels.Requests.Provider
{
    public class ProviderRequest : EntityRequestBase
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
