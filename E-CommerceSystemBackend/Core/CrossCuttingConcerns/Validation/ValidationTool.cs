using System;
using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        //Product Manager da yazabileceğimiz kodu CORE kısmında evrensel kod blokları ile yazdık
        //Validation işlemi için static bir Validation Tool kullandık.

        public static void Validate(IValidator validator,object entity)
        {
            var context = new ValidationContext<object>(entity);

            var result = validator.Validate(context); // ValidationResult Validate (IValidationContext context); 
                                                      // Parametre olarak context dosyası istiyor Abstract Validator içinde
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
