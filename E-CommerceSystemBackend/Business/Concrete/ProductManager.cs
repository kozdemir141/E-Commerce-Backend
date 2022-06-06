using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductManager:IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal) //Teknolojiye bağımlılığı Burada Dependency injection ile kaldırdık.
        {                                             //Bir nesneyi new'liyorsan orası tamamen o nesneye bağımlıdır.
            _productDal = productDal;                 //Bu bağımlılıkları Dependency Injection ile kaldırırız.
        }

        [ValidationAspect(typeof(ProductValidator),Priority =1)] //Operasyon başladığı anda,Aspect yapısı devreye giricek.Bunun için Aspect Yazıcaz
                                                     //Product Validator u kullanarak parametrede geçen Product Validator neyse(<Product>)
                                                     //Parametrede(Product product) aynısından bul ve bu Validator u kullarak Validate et.

        [CacheRemoveAspect("IProductService.Get")]
        //[SecuredOperation("Employee")]

        public IResult Add(Product product)
        {
            //ValidationTool.Validate(new ProductValidator(), product); //Bu noktada Validation Tool Aracı ile olayı merkezleştirdim.

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [SecuredOperation("Employee")]

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.productId == productId));
        }

        [CacheAspect(duration:2)]

        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.categoryId == categoryId).ToList());
        }

        //[ValidationAspect(typeof(ProductValidator), Priority = 1)]

        //[TransactionScopeAspect]

        [CacheRemoveAspect("IProductService.Get")] //İçerisinde IProductService.Get olan Cache Keylerini silecek

        //[SecuredOperation("Employee")]

        public IResult Update(Product product)
        {
            _productDal.Updatee(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
