using System;
namespace Core.Utilities.Security.Jwt
{
    //Bir kullanıcı sisteme Login olduğu zaman,biz login olan kullanıcının öncelikle login olma bilgilerini kontrol ediyor olucaz
    //Sonrasında bilgiler geçerliyse bu kullanıcıya Token veriyor olucaz.Bu Token JWT dediğimiz formata sahip
    //JWT=Jason Web Token =Şifreli bir formata sahip

    public class AccessToken //Access Token bir nesnedir.Erişim Token(Anahtarıdır.)
    {
        public string Token { get; set; } //Token ın kendisidir.

        public DateTime Expiration { get; set; } //Token'ın ne zamana kadar geçerli olduğu bilgisidir
    }
}
