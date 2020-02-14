using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClothsStore.Api
{
    public static class EncryptionManager
    {
        public static string salt = "fahnu3tg452!";
        public static string sha256(String valueToEncrypt)
        {
            // admin 909!
            HashAlgorithm hash = new SHA256Managed();

            // compute hash of the password prefixing password with the salt
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(salt + valueToEncrypt);
            byte[] hashBytes = hash.ComputeHash(plainTextBytes);

            string hashValue = Convert.ToBase64String(hashBytes);
            return hashValue;
        }
    }
}
