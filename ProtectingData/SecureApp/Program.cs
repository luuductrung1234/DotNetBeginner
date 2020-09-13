using System.Security;
using System.Security.Claims;
using System.Threading;
using CryptographyLib;
using static System.Console;

namespace SecureApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AuthProtector.Register("Alice", "Pa$$w0rd", new[] { "Admins" });
            AuthProtector.Register("Bob", "Pa$$w0rd", new[] { "Sales", "TeamLeads" });
            AuthProtector.Register("Eve", "Pa$$w0rd");

            if (!DoLogin())
                return;

            ShowCurrentPrincipal();

            try
            {
                RunSecuredAdminFeature();
            }
            catch (System.Exception ex)
            {
                WriteLine($"{ex.GetType()}: {ex.Message}");
            }
        }

        /// <summary>
        /// Simulated Authentication
        /// </summary>
        /// <returns></returns>
        static bool DoLogin()
        {
            Write($"Enter your user name: ");
            string username = ReadLine();
            Write($"Enter your password: ");
            string password = ReadLine();

            AuthProtector.LogIn(username, password);
            if (Thread.CurrentPrincipal == null)
            {
                WriteLine("Log in failed.");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Simulated Authorization
        /// </summary>
        static void RunSecuredAdminFeature()
        {
            if (Thread.CurrentPrincipal == null)
            {
                throw new SecurityException("A user must be logged in to access this feature.");
            }

            if (!Thread.CurrentPrincipal.IsInRole("Admins"))
            {
                throw new SecurityException("A user must be a member of Admins to access this feature.");
            }

            WriteLine("You have access to this secure feature.");
        }

        static void ShowCurrentPrincipal()
        {
            var p = Thread.CurrentPrincipal;
            WriteLine($"IsAuthenticated: {p.Identity.IsAuthenticated}");
            WriteLine($"AuthenticationType: {p.Identity.AuthenticationType}");
            WriteLine($"Name: {p.Identity.Name}");
            WriteLine($"IsInRole(\"Admins\"): {p.IsInRole("Admins")}");
            WriteLine($"IsInRole(\"Sales\"): {p.IsInRole("Sales")}");
            if (p is ClaimsPrincipal)
            {
                WriteLine($"{p.Identity.Name} has the following claims:");
                foreach (var claim in (p as ClaimsPrincipal).Claims)
                {
                    WriteLine($"{claim.Type}: {claim.Value}");
                }
            }
        }
    }
}
