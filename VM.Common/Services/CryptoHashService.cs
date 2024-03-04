using CryptoHelper;
using System.Security.Cryptography;

namespace VM.Common.Services
{
    public class CryptoHash
    {
        public string Salt { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;
        public static CryptoHash New(string hash, string salt)
        {
            return new CryptoHash
            {
                Hash = hash,
                Salt = salt
            };
        }
    }
    public interface ICryptoHashService
    {
        CryptoHash HashPassword(string password);
        bool VerifyPassword(string password, CryptoHash cryptoHash);
    }
    public class CryptoHashService : ICryptoHashService
    {
        public CryptoHash HashPassword(string password)
        {
            CryptoHash cryptoHash = new();
            cryptoHash.Salt = GetSalt();
            cryptoHash.Hash = Crypto.HashPassword(password + cryptoHash.Salt);
            return cryptoHash;
        }
        public bool VerifyPassword(string password, CryptoHash cryptoHash)
        {
            return Crypto.VerifyHashedPassword(cryptoHash.Hash, password + cryptoHash.Salt);
        }
        private string GetSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
