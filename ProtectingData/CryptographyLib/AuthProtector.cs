using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptographyLib
{
    public static class AuthProtector
    {
        public static User Register(string username, string password)
        {
            // generate a random salt (the same password but salted and hashed results are not the same)
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            var saltText = Convert.ToBase64String(saltBytes);

            // generate the salted and hashed password
            var saltedHashedPassword = SaltAndHashPassword(password, saltText);

            var user = new User
            {
                Name = username,
                Salt = saltText,
                SaltedHashedPassword = saltedHashedPassword
            };

            ApplicationContext.Users.Add(user.Name, user);

            return user;
        }

        public static bool CheckPassword(string username, string password)
        {
            if (!ApplicationContext.Users.ContainsKey(username))
                return false;

            var user = ApplicationContext.Users[username];

            // re-generate the salted and hashed password
            var saltedHashedPassword = SaltAndHashPassword(password, user.Salt);

            return (saltedHashedPassword == user.SaltedHashedPassword);
        }

        private static string SaltAndHashPassword(string password, string salt)
        {
            var sha = SHA256.Create();
            var saltedPassword = password + salt;
            return Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
        }
    }
}