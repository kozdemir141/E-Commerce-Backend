using System;
using Entities.Concrete;
using FluentValidation;
using System.Linq;

//3rd parti uygulamalarda klasörlemeyi unutma
namespace Business.ValidationRules.FluentValidation //Entity ve Dto ları anlatıyor olucak
{
    //Burada yazılan kodlar sadece Nesneyi (Entity Katmanı) ilgilendirir.

    public class ProductValidator:AbstractValidator<Product> //Base bir nesnedir,Fluent Validation Construcator ı sayesinde devreye soktuk
    {
        public ProductValidator()
        {
            RuleFor(p => p.productName).NotNull();
            RuleFor(p => p.productName).Length(3, 50);
            RuleFor(p => p.unitPrice).NotNull();
            RuleFor(p => p.unitPrice).GreaterThan(1);
            RuleFor(p => p.unitPrice).GreaterThanOrEqualTo(10).When(p => p.categoryId == 1);

            RuleFor(p => p.productName).Must(StartWithUpperCase);
        }

        private bool StartWithUpperCase(string arg)
        {
            return arg.StartsWith(Char.ToUpper(arg[0]));
        }
    }
}
