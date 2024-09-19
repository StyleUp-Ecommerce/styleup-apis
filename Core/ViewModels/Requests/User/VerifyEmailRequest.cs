using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.User
{
	public record VerifyEmailRequest(string Email, string Token);
}
