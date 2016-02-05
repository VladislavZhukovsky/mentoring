using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _02.TPL
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Task(Action1);
            var t1 = new Task(() => { Action2("333"); });
            t.Start();
            t1.Start();
            Console.ReadKey();
        }

        static void Action1()
        {

            Thread.CurrentThread.IsBackground = true;
            for (var k = 0; k != 10; k++)
            {
                Console.WriteLine("1 " + Task.CurrentId);
                Thread.Sleep(100);
            }
        }

        static void Action2(string s)
        {
            for (var k = 0; k != 10; k++)
            {
                Console.WriteLine(s + " " + Task.CurrentId);
                Thread.Sleep(500);
            }
        }

        static int Func1()
        {
            Console.WriteLine("func1");
            return 1;
        }

        static int Func2(int i)
        {
            Console.WriteLine("func2");
            return 2;
        }

        static void ParallelTest()
        {
            var n = 10000000;
            var array1 = Enumerable.Range(1, n).ToArray();
            var array2 = new double[n];
            Action<int> action = (x) => Math.Sqrt(Math.Pow(Math.Sqrt(x), 1 / 3));
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < n; i++)
            {
                action.Invoke(array1[i]);
            }
            sw.Stop();
            Console.WriteLine("FOR : " + sw.ElapsedMilliseconds);
            sw.Reset();
            sw.Start();
            foreach (var item in array1)
            {
                Math.Sqrt(Math.Pow(Math.Sqrt(item), 1 / 3));
            }
            sw.Stop();
            Console.WriteLine("FOREACH : " + sw.ElapsedMilliseconds);
            sw.Reset();
            sw.Start();
            Parallel.For(0, n - 1, (i) => { action(array1[i]); });
            //Parallel.ForEach<int>(array1, (item) => { Math.Pow(Math.Sqrt(item), 1 / 3); });
            sw.Stop();
            Console.WriteLine("PARALLEL FOR: " + sw.ElapsedMilliseconds);
            sw.Reset();
            sw.Start();
            Parallel.ForEach<int>(array1, (item) => { action(item); });
            sw.Stop();
            Console.WriteLine("PARALLEL FOREACH: " + sw.ElapsedMilliseconds);
            Console.ReadKey();
        }

        static void TaskVsThread()
        {
            var mre = new ManualResetEvent(false);
            var n = 1000;
            var array = Enumerable.Range(1, n + 1).ToArray();
            var action = new Action<int>(x => Math.Sqrt(Math.Sqrt(x)));
            var threads = new Thread[n];
            for (var i = 0; i < n; i++)
            {
                threads[i] = new Thread(() => { action(array[i]); mre.Set(); });
            }
            var sw = new Stopwatch();
            sw.Start();
            foreach (var t in threads)
            {
                t.Start();
            }
            //mre.WaitOne();
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Reset();
            mre.Reset();
            sw.Start();
            for (var i = 0; i < n; i++)
            {
                Task.Factory.StartNew(() => { action(array[i]); mre.Set(); });
            }
            mre.WaitOne();
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.ReadKey();
        }


    }
}
