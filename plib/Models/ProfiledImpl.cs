using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace plib.Models
{
    internal static class ProfiledImpl
    {
        public static async Task ProfileAsync(Func<Task> method)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await method();
            stopwatch.Stop();
            Console.WriteLine($"Метод выполнен за {stopwatch.ElapsedMilliseconds} мс");
        }

        public static void Profile(Action method)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            method();
            stopwatch.Stop();
            Console.WriteLine($"Метод выполнен за {stopwatch.ElapsedMilliseconds} мс");
        }
    }
}