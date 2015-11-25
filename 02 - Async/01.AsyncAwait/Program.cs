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


            var t = DoAsync();
            for (var i = 0; i < 50; i++)
            {
                Console.WriteLine("111 MAIN" + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            }
            Console.ReadKey();
        }

        static async Task<int> DoAsync()
        {
            //var t = Task.Run(() =>
            //{
            //    for (var i = 0; i < 10; i++)
            //    {
            //        Console.WriteLine("222 ASYNC" + Thread.CurrentThread.ManagedThreadId);
            //        Thread.Sleep(1000);
            //    }
            //});
            //await t;
            Console.WriteLine("777 MAIN" + Thread.CurrentThread.ManagedThreadId);
            await DoAsyncInner();
            for (var i = 0; i < 30; i++)
            {
                Console.WriteLine("333 ASYNC REMAINDER" + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            }
            return 1000000;
        }

        static async Task DoAsyncInner()
        {
            var t = Task.Run(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    Console.WriteLine("444 ASYNC INNER" + Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(1000);
                }
            });
            await t;
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("555 ASYNC INNER REMAINDER" + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            }
        }

        static async Task DoVoidAsync()
        {
            await Task.Delay(3000);
            Console.WriteLine("task ended");
        }


    }
}
