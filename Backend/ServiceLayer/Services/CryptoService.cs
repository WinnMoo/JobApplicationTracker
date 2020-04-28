using System;
using System.Security.Cryptography;

namespace ServiceLayer
{
    public static class CryptoService
    {
        public static string GenerateToken()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            Byte[] b = new byte[64 / 2];
            provider.GetBytes(b);
            string hex = BitConverter.ToString(b).Replace("-", "");
            return hex;
        }
    }
}
