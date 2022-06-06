using System;
using Entities.UserDtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class RegisterValidator:AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(u => u.FirstName).NotNull();
            RuleFor(u => u.FirstName).Length(3, 20);
            RuleFor(u => u.FirstName).Must(StartWithUpperCase);

            RuleFor(u => u.LastName).NotNull();
            RuleFor(u => u.LastName).Length(3, 20);
            RuleFor(u => u.LastName).Must(StartWithUpperCase);

            RuleFor(u => u.Email).EmailAddress();

            RuleFor(u => u.Password).MinimumLength(8);
        }
        private bool StartWithUpperCase(string arg)
        {
            return arg.StartsWith(Char.ToUpper(arg[0]));
        }
    }
}
