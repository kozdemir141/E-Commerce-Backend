using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption
{
    //SecurityKey=Biz bir algoritmayı kullanarak kendimize şifreli bir token oluşturucaz.
    //Token ı oluştururken kendi bildiğimiz özel bir anahtara ihtiyacımız var.Onu Kullanıcaz
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
