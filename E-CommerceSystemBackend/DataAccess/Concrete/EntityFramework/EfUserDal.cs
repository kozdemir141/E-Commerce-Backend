using System;
using System.Collections.Generic;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Concrete.User;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        //Kullanıcının Rollerini Çekmek İstiyoruz.

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context=new NorthwindContext())
            {
                var result = from operationClaim in context.OperationClaims         //OperationClaims ile
                             join userOperationClaim in context.UserOperationClaims //UserOperationClaims tablosunu join etmem gerekiyor.

                             on operationClaim.id equals userOperationClaim.operationClaimId
                             where userOperationClaim.userId == user.userId

                             select new OperationClaim { id = operationClaim.id, name = operationClaim.name };
                             //OperationClaim Dto Class Görevi görüyor,İhtiyaca göre veri tabanına ekleme yapılabilir.

                return result.ToList();
            }
        }
        //Bu şekilde gönderdiğimiz Userın claimlerini GetClaims operasyonu vasıtası ile çektik.
    }
}
