using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Ultrix.Application.Interfaces;

namespace Ultrix.Application.Cryptography
{
    public class Pbkdf2Hasher : IHasher
    {
        public string Hash(string secret, byte[] saltBytes)
        {
            return ComputeHash(secret, saltBytes);
        }

        public bool VerifyHash(string storedHash, string storedSalt, string secret)
        {
            byte[] salt = Convert.FromBase64String(storedSalt);
            return ComputeHash(secret, salt).Equals(storedHash);
        }

        private static string ComputeHash(string secret, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(secret, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));
        }
    }
}
