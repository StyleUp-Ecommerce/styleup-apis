using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.Voucher
{
    public class CheckValidUserResponse
    {
        public bool IsValid {  get; set; }
        public string Message { get; set; }
    }
}
