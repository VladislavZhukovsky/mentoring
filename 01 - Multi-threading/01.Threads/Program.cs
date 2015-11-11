using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _01.Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(() => Do("THREAD")).Start();
            new Task(() => Do("TASK")).Start();
            ThreadPool.QueueUserWorkItem((arg) => Do("POOL"));
        }

        static void Do(string s)
        {
            for (var i = 0; i < 10; i++)
            {
                var output = s;
                output += ' ';
                if (Thread.CurrentThread.IsThreadPoolThread)
                    output += "POOL";
                Console.WriteLine(output);
                Thread.Sleep(1000);
            }
        }

    }
}
