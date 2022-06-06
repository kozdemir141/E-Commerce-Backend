using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Core.Extensions
{
    //Extensions bir Class'ı veya nesneyi genişletmek demektir.<Claim>nesnesini genişleticez

    public static class ClaimExtensions//Genişletilen Nesne
    {
        //Claim nesnesini gemişlettik.Jwt Helper/Set Claims içinde artık claims.AddEmail operasyonunu çekebileceğiz

        public static void AddEmail(this ICollection<Claim> claims,string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email)); //Yeni bir Claim ekledik.
                             //Kayıtlı isimlerdir.
        }

        public static void AddName(this ICollection<Claim> claims,string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims,string nameidentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameidentifier));
        }

        public static void AddRoles(this ICollection<Claim> claims,string[]roles)
        {
          //Roles array olarak geliyor Listeye çevir.
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
          //Her bir role için(Foreach uygula) her bir rolü claimlere ekle.
        }
    }
}
