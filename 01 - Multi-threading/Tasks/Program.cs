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
        static List<int> list = new List<int>();
        static Semaphore sem = new Semaphore(0, 10);
        static AutoResetEvent are1 = new AutoResetEvent(true);
        static AutoResetEvent are2 = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            var t1 = new Thread(AddElement);
            var t2 = new Thread(Print);
            t1.Start();
            t2.Start();
            Console.Read();
        }

        static void AddElement()
        {
            for(var i = 0; i < 10; i++)
            {
                are1.WaitOne();
                list.Add(i);
                are2.Set();
            }
        }

        static void PrintAll()
        {
            for (var i = 0; i < list.Count; i++)
            {
                Console.Write(list[i]);
                Console.Write(' ');
            }
            Console.WriteLine();
        }

        static void Print()
        {
            for (var i = 0; i < 10; i++)
            {
                are2.WaitOne();
                PrintAll();
                are1.Set();
            }
        }

        #region Task1

        //static void Task1()
        //{
        //    int n = 10;
        //    Do(n);
        //    Console.WriteLine("end");
        //    Console.ReadKey();
        //}

        //static void Do(object n)
        //{
        //    Thread.Sleep(500);
        //    Console.WriteLine((int)n);
        //    if ((int)n != 0)
        //    {
        //        var t = new Thread(x => Do((int)n - 1));
        //        t.Start();
        //        t.Join();
        //    }
        //}

        #endregion
    }
}
