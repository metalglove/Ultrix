namespace Ultrix.Application.Interfaces
{
    public interface IHasher
    {
        bool VerifyHash(string storedHash, string storedSalt, string secret);
        string Hash(string secret, byte[] saltBytes);
    }
}
