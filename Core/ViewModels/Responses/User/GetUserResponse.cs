using CleanBase.Core.ViewModels.Response.Base;

namespace Core.ViewModels.Responses.User
{
    public class GetUserResponse : EntityResponseBase
    {
        public int Age { get; set; }
        public string Message { get; set; }
    }
}
