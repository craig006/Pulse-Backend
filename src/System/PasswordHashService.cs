using System;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace ServeUp.System
{
    public class PasswordHashService : IPasswordHashService, IDisposable
    {   
        private RNGCryptoServiceProvider _cryptoServiceProvider = new RNGCryptoServiceProvider();

        private const int SaltSize = 16;

        private const int HashSize = 20;

        private const int iterations = 1000;

        public string Hash(string password)
        {
            byte[] salt = new byte[SaltSize];
            _cryptoServiceProvider.GetBytes(salt);

            var derrivedKey = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = derrivedKey.GetBytes(HashSize);

            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }
        
        public bool Verify(string password, string hashedPassword)
        {
            var hashBytes = Convert.FromBase64String(hashedPassword);

            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var derrivedKey = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = derrivedKey.GetBytes(HashSize);

            return hash.SequenceEqual(hashBytes.Skip(SaltSize));
        }

        public void EnsureVerified(string password, string hashedPassword)
        {
            if(!Verify(password, hashedPassword))
            {
                throw new SecurityException();
            }
        }

        public void Dispose()
        {
            _cryptoServiceProvider.Dispose();
        }
    }
}