using System;
using System.Diagnostics;
using System.Linq;

namespace LinqInParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            System.Console.Write("Press ENTER to start: ");
            System.Console.ReadLine();
            watch.Start();

            var numbers = Enumerable.Range(1, 2_000_000_000);
            var squareNumbers = numbers.AsParallel().Select(number => number * number).ToArray();

            watch.Stop();
            System.Console.WriteLine("{0:#,##0} elapsed milliseconds.", watch.ElapsedMilliseconds);
        }
    }
}
