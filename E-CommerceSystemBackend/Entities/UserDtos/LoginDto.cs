using System;
using Core.Entities.Abstract;

namespace Entities.UserDtos
{
    //İlk işlemimiz bir kullanıcının sisteme Login  veya Register olması durumunda yapılacak çalışmadır
    //Login veya Register olma durumunda bilgiler istenicek(Email,password vs)
    //Bu tip sadece işimizi görücek yapılarda DTO lardan yararlanırız.
    public class LoginDto:IDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
