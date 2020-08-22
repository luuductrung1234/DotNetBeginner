using System;

namespace SecureApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Protector.Register("Alice", "Pa$$w0rd", new[] { "Admins" });
            Protector.Register("Bob", "Pa$$w0rd", new[] { "Sales", "TeamLeads" });
            Protector.Register("Eve", "Pa$$w0rd");
            Write($"Enter your user name: ");
            string username = ReadLine();
            Write($"Enter your password: ");

            Protector.LogIn(username, password);
            if (Thread.CurrentPrincipal == null)
            {
                WriteLine("Log in failed.");
                return;
            }

            var p = Thread.CurrentPrincipal;
            WriteLine($"IsAuthenticated: {p.Identity.IsAuthenticated}");
            WriteLine($"AuthenticationType: {p.Identity.AuthenticationType}");
            WriteLine($"Name: {p.Identity.Name}");
            WriteLine($"IsInRole(\"Admins\"): {p.IsInRole("Admins")}");
            WriteLine($"IsInRole(\"Sales\"): {p.IsInRole("Sales")}");
            if (p is ClaimsPrincipal)
            {
                WriteLine(
                $"{p.Identity.Name} has the following claims:");
                foreach (Claim in (p as ClaimsPrincipal).Claims)
                {
                    WriteLine($"{claim.Type}: {claim.Value}");
                }
            }
        }
    }
}
