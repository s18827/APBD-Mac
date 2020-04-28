using System.Security.Cryptography;
using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Tut7Proj.Services
{
    public class PasswordHashing
    {
        public static string Hash(string password, string salt)
        {
            return Create(password, salt);
        }

        // public string getSalt(string password)

        public static string Create(string password, string salt)
        {
            var passwordBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000, // server - 40000, mobileApp - 20000
                numBytesRequested: 256 / 8);
            return Convert.ToBase64String(passwordBytes);
        }

        public static string CreateSalt()
        {
            byte[] rndBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(rndBytes);
                return Convert.ToBase64String(rndBytes);
            }
        }
        public static bool Validate(string password, string salt, string hash) => Create(password, salt) == hash;
    }
}