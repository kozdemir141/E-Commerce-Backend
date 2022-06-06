using System;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper//Algoritmamızı belirlediğimiz nesnedir.
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }                                //Anahtar    //Algoritma=Encrypt edilen datanın nasıl encrypt edildiği ile ilgilidir.
    }
}
