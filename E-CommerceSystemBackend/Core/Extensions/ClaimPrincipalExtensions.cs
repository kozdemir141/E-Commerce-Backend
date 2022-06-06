using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimPrincipalExtensions //Mevcut kullanıcımıza karşılık gelir.Erişilecek hedef noktası "Roles" dür.
    {
        //Bir kullanıcının mevcut rollerine ClaimPrincipal ile erişilir.

        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal,string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            //ClaimPrincipal var mı? Claim Type istedi verdi buda varsa Select et. Her bir ClaimType ın Key value değerini oku ve listele.

            return result; //Bununla birlikte istediğim ClaimType erişebilirim.
        }

        public static List<string> ClaimRoles (this ClaimsPrincipal claimsPrincipal) //Claims Methodundaki this parametresine dikkat(Extend ettik)
        {
            return claimsPrincipal.Claims(ClaimTypes.Role);
        }
    }
}
