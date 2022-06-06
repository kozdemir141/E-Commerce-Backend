using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities.Abstract;

namespace Core.DataAccess
{
    //Uygulamamzın her bir noktasında gerekli veritabanı işlemlerini tekrar yazmak yerine Repository Pattern ile ortak desende yazıldı.

    public interface IEntityRepository<T> where T:class,IEntity,new()//Generic Kısıtlar
    {
        T Get(Expression<Func<T, bool>> filter);
        //Her türlü parametreyi gönderebilmek için LinqExpression kullandık.
        //Func delegasyonu kullandık T tipinde değer alıp bool tipinde değer döndürür.

        IList<T> GetList(Expression<Func<T, bool>> filter = null);
        //Nullable,Filtre(parametre) gönderilmezse tümümü listele.

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
