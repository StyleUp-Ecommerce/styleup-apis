using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.User
{
	public record ResetPasswordRequest(string Email, string Token, string Password);
}
