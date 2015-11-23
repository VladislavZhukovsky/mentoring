using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _01.AsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            var range = Enumerable.Range(1, 10000000);
            var query = range.AsParallel().Where(x => x % 2 == 1);
            var array = query.ToArray();

            var t = Do();
            Console.WriteLine(10000000);
            Console.ReadKey();
        }

        static async Task<int> Do()
        {
            await Task.Run(() => 
            {
                for (var i = 0; i < 10; i++)
                {
                    Console.WriteLine(i);
                    Thread.Sleep(1000);
                }
            });
            return 1000000;
        }

    }
}
