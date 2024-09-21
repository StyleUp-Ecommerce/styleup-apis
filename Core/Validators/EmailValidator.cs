using FluentValidation;

namespace Core.Validators
{
    public partial class EmailValidator : AbstractValidator<string>
    {
        public EmailValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().EmailAddress();
        }
    }
}