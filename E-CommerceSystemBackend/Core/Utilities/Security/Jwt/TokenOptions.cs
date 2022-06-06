using System;
namespace Core.Utilities.Security.Jwt
{
    public class TokenOptions
    //Nesne üzerinden map edip,bu nesne üzerinden kullancağız.
    //Apimizde settings içinde tutucaz.
    //Jason Web Token ın belirli standartları vardır bu standartları veriyor olmamız lazım.
    {
        public string Audience { get; set; } //Kullanıcı kitlesi

        public string Issuer { get; set; } //İmzalayan

        public int AccessTokenExpiration { get; set; } //Geçerlilik süresi (Dakika Cinsinden)

        public string SecurityKey { get; set; }

    }
}
