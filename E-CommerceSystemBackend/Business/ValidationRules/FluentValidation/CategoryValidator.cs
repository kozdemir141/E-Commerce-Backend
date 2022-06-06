using System;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CategoryValidator:AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.categoryName).NotNull();
            RuleFor(c => c.categoryName).Length(3, 30);

            RuleFor(c => c.categoryName).Must(StartWithUpperCase);
        }

        private bool StartWithUpperCase(string arg)
        {
            return arg.StartsWith(char.ToUpper(arg[0]));
        }
    }
}
