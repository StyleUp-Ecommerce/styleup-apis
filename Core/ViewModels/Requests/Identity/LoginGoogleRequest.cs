using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.Identity
{
    public class LoginGoogleRequest
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
