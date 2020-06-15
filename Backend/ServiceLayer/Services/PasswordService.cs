using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ServiceLayer.Services
{
    public class PasswordService
    {
        public byte[] GenerateSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            return salt;
        }

        public string HashPassword(string password, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string passwordHash = Convert.ToBase64String(hashBytes);
            return passwordHash;
        }

        public bool validatePassword(string passwordToCheck, byte[] passwordSalt, string passwordHash)
        {
            var hashedPasswordToCheck = HashPassword(passwordToCheck, passwordSalt);
            if (passwordHash.Equals(hashedPasswordToCheck))
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
