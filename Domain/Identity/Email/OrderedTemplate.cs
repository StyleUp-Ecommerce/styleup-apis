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
    public class OrderedTemplate : IEmailClassifier
    {
        public bool Classified(EmailType type) => EmailType.Ordered == type;

        public ClassifiedEmail GetEmail(IDictionary<string, string> parameters)
        {
            ClassifiedEmail classified =
                new()
                {
                    Subject = EmailSubject.Ordered,
                    Body = EmailBody.Ordered(
                        parameters["fullName"],
                        parameters["orderCode"],
                        parameters["totalPrice"], 
                        parameters["recipientName"],
                        parameters["orderStatus"],
                        parameters["recipientEmail"],
                        parameters["recipientPhone"],
                        parameters["address"]
                    )
                };

            return classified;
        }
    }
}
