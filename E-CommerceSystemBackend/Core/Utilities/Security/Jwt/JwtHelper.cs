using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Core.Entities.Concrete;
using Core.Entities.Concrete.Employee;
using Core.Entities.Concrete.User;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; } //IConfiguration dosyası sayesinde biz WebAPI mizden gelen bilgiyi okucaz.
                                                     //WebAPI içindeki appsettings.json içindeki "TokenOptions" bilgilerini okucaz.

        TokenOptions _tokenoptions; //WebAPI/appsettings.json dan gelen "TokenOptions" bilgilerini _tokenoptions(TokenOptions) nesnesine aktarıcaz

        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration) //IConfiguration u henüz kodlamadık,API tarafında konfigurasyon eksiğimiz var
        {                                              //IConfiguration u kodlamamız lazım.
            this.Configuration = configuration;

            //Apı den gelen "TokenOptions" bilgilerini nesneye bind etti.

            _tokenoptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); //Her şeyi this.TokenOptions nesnesine atmış olduk.
                                      //Sectionı oku-API/appsettings.json-Ve onu bu nesneye bağla

            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenoptions.AccessTokenExpiration);//Artık elimizde bir tarih var.Biz bunu Token Options da int olarak belirtmiştik
            //Her seferinde kullanacağımız için Constructor da set ettik                         //Operasyon ile tarih dakika cinsine çevirdik.
        }


        //USER-------------------------------------------------------------------------------------------------------------------------

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenoptions.SecurityKey);//Artık kendi oluşturduğumuz özel bir anahtar mevcut

            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

            var jwt = CreateJwtSecurityToken(_tokenoptions,user,signingCredentials,operationClaims);
            //Elimizde bir "Token" mevcut ama bizim o Token ı elimizdeki bilgilere göre bir handler vasıtası ile yazmamız gerekiyor.

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler(); //Identity Model dan geliyor

            var token = jwtSecurityTokenHandler.WriteToken(jwt);//Elimizdeki bilgileri Token a yazdırdık
                                                                //Write Token ile bilgileri string e çevirdi.
            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        //Elimizdeki bilgileri kullanarak Bir SecurityToken oluşturucaz.
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,User user,SigningCredentials signingCredentials,List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken //Bilgiler Parametre olarak kayıtlı içinde ihtiyaçlara göre parametrelerini belirle
                (
                issuer:_tokenoptions.Issuer,
                audience:_tokenoptions.Audience,
                expires:_accessTokenExpiration,
                notBefore:DateTime.Now,//Şarttır;Eğer Token Expiration bilgisi şu andan önce ise geçerli değildir

                //Bizden (IEnumarable)claimler istiyor.Claim türünde olucak
                //Bizden IEnumareble<Claim> istiyor.Bu yüzden Claim set edebileceğimiz bir operasyon yazıcaz.
                claims:SetClaims(user,operationClaims),
                signingCredentials:signingCredentials
                );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user,List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            //claims.Add(new Claim("email", user.email)); Böyle yazarsak da çalışır.Ama Claim nesnesini Extend edip daha Profesyonel hale getiricez.

            claims.AddNameIdentifier(user.userId.ToString());
            claims.AddEmail(user.email);
            claims.AddName($"{user.firstName}{ user.lastName}");

            //OperationClaim bir koleksiyon,Linq operasyonu ile claim'in name'ini getirdik.ICollection olur
            claims.AddRoles(operationClaims.Select(c=>c.name).ToArray());

            return claims;
        }

        //EMPLOYEE--------------------------------------------------------------------------------------------------------------------

        public AccessToken CreateTokenForEmployee(Employee employee, List<OperationClaim> operationClaims)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenoptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

            var jwt = CreateJwtSecurityTokenForEmployee(_tokenoptions, employee, signingCredentials, operationClaims);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityTokenForEmployee(TokenOptions tokenOptions,Employee employee,SigningCredentials signingCredentials,List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken
                (
                issuer: _tokenoptions.Issuer,
                audience: _tokenoptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaimsForEmployee(employee, operationClaims),
                signingCredentials: signingCredentials
                );
            return jwt;
        }

        private IEnumerable<Claim> SetClaimsForEmployee(Employee employee,List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();

            claims.AddNameIdentifier(employee.employeeId.ToString());
            claims.AddEmail(employee.email);
            claims.AddName($"{employee.firstname}{ employee.lastname}");

            claims.AddRoles(operationClaims.Select(c => c.name).ToArray());

            return claims;
        }
    }
}
