using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptographyLib
{
    public static class MessageProtector
    {
        // salt is a random byte array that is used as an additional input to one-way hash function
        // salt size must be at least 8 bytes
        private static readonly byte[] salt = Encoding.Unicode.GetBytes("7BANANAS");

        // iterations must be at least 1000
        private static readonly int iterations = 2000;

        /// <summary>
        /// Symmetric encryption
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="password">never hardcode a password in source code, because password can be read in the assembly by using disassembler tools</param>
        /// <returns></returns>
        public static string Encrypt(string plainText, string password)
        {
            byte[] encryptedBytes;
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);

            // Advanced Encryption Standard (AES), algorithm for symmetric encryption
            var aes = Aes.Create();

            // generating keys and IVs, using Password-base Key Derivation Function (PBKDF2)
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);

            // single key for both encypt & decrypt in symmetric encryption
            aes.Key = pbkdf2.GetBytes(32); // set a 256-bit key

            // Initialization Vector (IV) dividing password into blocks
            aes.IV = pbkdf2.GetBytes(16); // set 128-bit IV

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plainBytes, 0, plainBytes.Length);
                }
                encryptedBytes = ms.ToArray();
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// Symmetric decryptiong
        /// </summary>
        /// <param name="cryptoText"></param>
        /// <param name="password">never hardcode a password in source code, because password can be read in the assembly by using disassembler tools</param>
        /// <returns></returns>
        public static string Decrypt(string cryptoText, string password)
        {
            byte[] plainBytes;
            byte[] cryptoBytes = Convert.FromBase64String(cryptoText);

            // Advanced Encryption Standard (AES), algorithm for symmetric encryption
            var aes = Aes.Create();

            // generating keys and IVs, using Password-base Key Derivation Function (PBKDF2)
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);

            // single key for both encypt & decrypt in symmetric encryption
            aes.Key = pbkdf2.GetBytes(32); // set a 256-bit key

            // Initialization Vector (IV) dividing password into blocks
            aes.IV = pbkdf2.GetBytes(16); // set 128-bit IV

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cryptoBytes, 0, cryptoBytes.Length);
                }
                plainBytes = ms.ToArray();
            }

            return Encoding.Unicode.GetString(plainBytes);
        }
    }
}
