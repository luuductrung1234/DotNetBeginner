using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptographyLib
{
    /// <summary>
    /// To prove that some data has come from someone we trust, it can bet signed.
    /// 
    /// Sign a hash of the data.
    /// </summary>
    public class SignatureProtector
    {
        public static string PublicKey;

        public static string GenerateSignature(string data)
        {
            byte[] dataBytes = Encoding.Unicode.GetBytes(data);
            var sha = SHA256.Create();

            var hashedData = sha.ComputeHash(dataBytes);

            var rsa = RSA.Create();
            PublicKey = rsa.ToXmlString(false); // exclude private key

            return Convert.ToBase64String(rsa.SignHash(hashedData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
        }

        public static bool ValidateSignature(string data, string signature)
        {
            byte[] dataBytes = Encoding.Unicode.GetBytes(data);
            var sha = SHA256.Create();

            var hashedData = sha.ComputeHash(dataBytes);

            byte[] signatureBytes = Convert.FromBase64String(signature);

            var rsa = RSA.Create();
            rsa.FromXmlString(PublicKey);

            return rsa.VerifyHash(hashedData, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }
}