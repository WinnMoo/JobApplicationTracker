using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UnitTests
{
    public class TestUtilities
    {
        public byte[] GenerateRandomBytes()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        public UserAccount CreateUser()
        {
            var user = new UserAccount
            {
                FirstName = "Winn",
                Email = "winn@example.org",
                PasswordHash = (Guid.NewGuid()).ToString(),
                PasswordSalt = GenerateRandomBytes(),
                SecurityQuestion1 = "SecurityQuestion1",
                SecurityQuestion2 = "SecurityQuestion2",
                SecurityQuestion3 = "SecurityQuestion3",
                SecurityAnswer1 = "SecurityAnswer1",
                SecurityAnswer2 = "SecurityAnswer1",
                SecurityAnswer3 = "SecurityAnswer1"
            };
            return user;
        }
    }
}
