﻿using System;
using CryptographyLib;
using static System.Console;

namespace HashingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Registering Alice with Pa$$w0rd.");
            var alice = AuthProtector.Register("Alice", "Pa$$w0rd");
            WriteLine($"Name: {alice.Name}");
            WriteLine($"Salt: {alice.Salt}");
            WriteLine("Password (salted and hashed): {0}",
            arg0: alice.SaltedHashedPassword);
            WriteLine();

            Write("Enter a new user to register: ");
            string username = ReadLine();
            Write($"Enter a password for {username}: ");
            string password = ReadLine();
            var user = AuthProtector.Register(username, password);
            WriteLine($"Name: {user.Name}");
            WriteLine($"Salt: {user.Salt}");
            WriteLine("Password (salted and hashed): {0}",
            arg0: user.SaltedHashedPassword);
            WriteLine();

            bool correctPassword = false;
            while (!correctPassword)
            {
                Write("Enter a username to log in: ");
                string loginUsername = ReadLine();
                Write("Enter a password to log in: ");
                string loginPassword = ReadLine();
                correctPassword = AuthProtector.CheckPassword(
                loginUsername, loginPassword);
                if (correctPassword)
                {
                    WriteLine($"Correct! {loginUsername} has been logged in.");
                }
                else
                {
                    WriteLine("Invalid username or password. Try again.");
                }
            }
        }
    }
}
