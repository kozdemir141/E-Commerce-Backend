using System;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using FluentValidation;
using Core.Utilities.Messages;
using System.Linq;
using Core.CrossCuttingConcerns.Validation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect:MethodInterception
    {
        //[ValidationAspect(typeof(ProductValidator))]

        Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //Gönderilen validatorType IValidator türünde değilse.
            {
                throw new Exception(AspectMessages.WrongValidationType);
            }
            _validatorType = validatorType;
        }

        //OnBefore un İÇİNİ DOLDURDUĞUM ANDA,OVERRIDE ETTİĞİM ANDA ValidationAspect in olduğu yerde OnBefore çalışacak
        //Bizim Aspect yapımız OnBefore bir nevi.

        //ValidationTool.Validate(new ProductValidator(), product);

        public override void OnBefore(IInvocation invocation) //invocation bizim Methodumuz (public IResult Add(Product product))
        {
                                                                                 //IValidator un olması gerekiyor operasyonlarını kullanıcaz.
            var validator = (IValidator)Activator.CreateInstance(_validatorType);//Instance üretiyor reflökter yöntemi ile.
                                                                                 //Validator a ulaştık ve gönderilen Validator u bellekte newledik.=>new ProductValidator();

            var entityType = _validatorType.BaseType.GetGenericArguments()[0];//Nesneye ulaştık(Product)
                                                                              //Validator a git Base type (AbstractValidator<Product>) ın GenericArgumanını,Nesnesini(Parametresini) al.

            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);//Git methodun argümanlarına bak(parametlerine) ve orayı filtrele

            foreach (var entity in entities)//Birden fazla argüman(parametre) varsa tek tek Validate et.
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
