using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 10;
            Do(n);
            Console.WriteLine("end");
            Console.ReadKey();
        }

        static void Do(object n)
        {
            Thread.Sleep(500);
            Console.WriteLine((int)n);
            if ((int)n != 0)
            {
                var t = new Thread(x => Do((int)n - 1));
                t.Start();
                t.Join();
            }
        }
    }
}
