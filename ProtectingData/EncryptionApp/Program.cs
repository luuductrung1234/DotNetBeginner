using System;
using System.Security.Cryptography;
using CryptographyLib;
using static System.Console;

namespace EncryptionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter a message that you want to encrypt: ");
            string message = ReadLine();

            Write("Enter a password: ");
            string password = ReadLine();

            string cryptoText = MessageProtector.Encrypt(message, password);
            WriteLine($"Encrypted text: {cryptoText}");

            Write("Enter the password: ");
            string password2 = ReadLine();

            try
            {
                string clearText = MessageProtector.Decrypt(cryptoText, password2);
                WriteLine($"Decrypted text: {clearText}");
            }
            catch (CryptographicException ex)
            {
                WriteLine("{0}\nMore details: {1}",
                    arg0: "You entered the wrong password!",
                    arg1: ex.Message);
            }
            catch (Exception ex)
            {
                WriteLine("Non-cryptographic exception: {0}, {1}",
                    arg0: ex.GetType().Name,
                    arg1: ex.Message);
            }
        }
    }
}
