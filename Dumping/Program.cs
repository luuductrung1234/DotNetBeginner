using System;
using System.Threading.Tasks;
using SharpPad;

namespace Dumping
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var complexObject = new
            {
                FirstName = "Thomas",
                BirthDate = new DateTime(2000, 1, 1),
                Friends = new[] { "Yen", "Sang", "Tuyen" }
            };

            System.Console.WriteLine($"Dupming {nameof(complexObject)} to SharpPad.");

            await complexObject.Dump();
        }
    }
}
