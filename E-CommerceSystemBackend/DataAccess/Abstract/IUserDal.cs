using System;
using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.Concrete.User;

namespace DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user); //User kullanıcısının rollerini çekmek istiyoruz

        //Parametre olarak verdiğimiz User ın sahip olduğu claimleri çekicez.
        //Bir Join Operasyonu olucak.
    }
}
