using CleanBase.Core.ViewModels.Request.Base;

namespace Core.ViewModels.Requests.Pet
{
    public class PetRequest : EntityRequestBase
    {
        public int Age { get; set; }
    }
}
