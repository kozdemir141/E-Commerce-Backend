using System;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    //Neye ne zaman ihtiyacımız varsa yazıyoruz,İhtiyaca göre programlama="Intensional Programming".
    //Hashing bütün projelerde kullanılır evrenseldir.
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) //Hash oluşturacağımız zaman bu parametreler standarttır.
        //"out" keyword=Parametreyi gönderdiğimizde değişen nesne aynı zamanda bizim byte[]array imizde aktarılacak.
        //Kısacası Hash ve Salt'un son halini oluşturacak.
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())//Hashing Algoritmamız
            {
                passwordSalt = hmac.Key;//ANAHTARIMIZ.PasswordSalt ı değiştirdik.
                //Key oluşturuldu,Salt vasıtası ile biz bunu passwordSalt a aktardık,out sayesinde nesne byte[]passwordSalt a aktarıldı.

                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//Elimizde artık CreateHash mevcut.
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)//Verify yapacağımız için out keyword kullanılmaz.
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++) //İki Hash i for döngüsü ile karşılaştırdık
                {
                    if (computedHash[i] != passwordHash[i]) //Kullanıcı bize bir şifre gönderdi,daha önce oluşanla (out[]passwordHash) birbiri ile örtüşüyor mu?
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
