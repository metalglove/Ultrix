using System.Security.Cryptography;
using Ultrix.Application.Interfaces;

namespace Ultrix.Application.Cryptography
{
    public class RandomSaltGenerator : ISaltGenerator
    {
        public byte[] GenerateSalt()
        {
            byte[] saltAsBytes = new byte[128 / 8];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                rng.GetBytes(saltAsBytes);

            return saltAsBytes;
        }
    }
}
