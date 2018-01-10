using System;
using System.Security.Cryptography;
using Blog.Data.Security;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;

namespace Blog.Features.Security
{
    public class PasswordHasher : IPasswordHasher<BlogUser>
    {
        public string HashPassword(BlogUser user, string password)
        {
            return HashPasswordV1(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(BlogUser user, string hashedPassword, string providedPassword)
        {
            // Convert the stored Base64 password to bytes
            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

            // The first byte indicates the format of the stored hash
            if (decodedHashedPassword[0] == 1)
            {
                var newHash = HashPasswordV1(providedPassword, hashedPassword);
                if (newHash == hashedPassword)
                {
                    return PasswordVerificationResult.Success;
                }
            }
            return PasswordVerificationResult.Failed;
        }

        private static string HashPasswordV1(string password, string hashedPassword = null)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA512;
            const int pbkdf2IterCount = 11548;
            const int Pbkdf2SubkeyLength = 512 / 8; // 512 bits
            const int SaltSize = 256 / 8; // 256 bits

            byte[] salt = new byte[SaltSize]; 
            if (hashedPassword == null)
            {
                // create new salt
                var rng = RandomNumberGenerator.Create();
                rng.GetBytes(salt);
            }
            else
            {
                // use salt from existing password hash (start at position 1 as 0 is version)
                Array.Copy(Convert.FromBase64String(hashedPassword), 1, salt, 0, SaltSize); 
            }
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, pbkdf2IterCount, Pbkdf2SubkeyLength);

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
            outputBytes[0] = 0x01; // format marker - V1
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }

    }
}
