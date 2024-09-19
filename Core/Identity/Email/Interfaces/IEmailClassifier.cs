using Core.Identity.Email.Enums;
using Core.Identity.Email.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Identity.Email.Interfaces
{
	public interface IEmailClassifier
	{
		bool Classified(EmailType type);
		ClassifiedEmail GetEmail(IDictionary<string, string> parameters);
	}
}
