using System;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public void Updatee(Product product)
        {
            using (var context=new NorthwindContext())
            {
                var currentProduct = context.Product.AsNoTracking().SingleOrDefault(p => p.productId == product.productId);

                var updatedProduct = new Product
                {
                    productId = product.productId,
                    IsDeleted = currentProduct.IsDeleted,
                    productName = (product.productName == null) ? currentProduct.productName : product.productName,
                    categoryId = (product.categoryId == 0) ? currentProduct.categoryId : product.categoryId,
                    quantityPerUnit = (product.quantityPerUnit == null) ? currentProduct.quantityPerUnit : product.quantityPerUnit,
                    unitPrice = (product.unitPrice == 0) ? currentProduct.unitPrice : product.unitPrice,
                    unitsInStock = (product.unitsInStock == 0) ? currentProduct.unitsInStock : product.unitsInStock
                };
                var entity = context.Entry(updatedProduct);
                entity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
