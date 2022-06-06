using System;
using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Employee;
using Core.Entities.Concrete.User;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper//Token üretimi gerçekleştirecek bir Helper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);//User bilgisine göre Token üreticek.
                                                                                 //Kullanıcı rollerini verip Token a eklenmesini istiyoruz
        AccessToken CreateTokenForEmployee(Employee employee, List<OperationClaim> operationClaims);
    }                                                                            //Kullanıcı rollerini verip Token a eklenmesini istiyoruz
                                                                                 //OperationClaim bizim için Dto görevi gören bir nesneydi
                                                                                 //EfUserDal da bunu GetClaims operasyonu içerisinde joinleyerek Kullanıcı Claimlerini atamıştık.


}
