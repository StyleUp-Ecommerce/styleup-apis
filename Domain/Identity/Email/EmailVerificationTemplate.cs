using CleanBase.Core.Identity.Email.Constants;
using Core.Identity.Email.Enums;
using Core.Identity.Email.Interfaces;
using Core.Identity.Email.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity.Email
{
	public class EmailVerificationTemplate : IEmailClassifier
	{
		public bool Classified(EmailType type) => EmailType.Verification == type;

		public ClassifiedEmail GetEmail(IDictionary<string, string> parameters)
		{
			ClassifiedEmail classified =
				new()
				{
					Subject = EmailSubject.Verification,
					Body = EmailBody.Verification(parameters["fullName"], parameters["url"])
				};

			return classified;
		}
	}
}
