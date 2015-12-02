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
            
            Console.ReadKey();
        }

        static async Task<object> GetObject(bool b)
        {
            Task<object> task = null;
            if (b)
                task = Task.Run(() => { return new object(); });
            object result = null;
            if (task != null)
            {
                result = await task;
            }
            return result;
        }

        static async Task<object> GetObject1(bool b)
        {
            var task = b ? Task.Run(() => { return new object(); }) : Empty<object>.Task;
            return await task;
        }

        public static class Empty<T>
        {
            public static Task<T> Task { get { return _task; } }

            private static readonly Task<T> _task = System.Threading.Tasks.Task.FromResult(default(T));
        }



        //static async Task<int> DoAsync()
        //{
        //    var t = Task.Run(() =>
        //    {
        //        for (var i = 0; i < 10; i++)
        //        {
        //            Console.WriteLine("ASYNC" + Thread.CurrentContext.ContextID.ToString());
        //            Thread.Sleep(1000);
        //        }
        //    });

        //    await t;
        //    for (var i = 0; i < 10; i++)
        //    {
        //        Console.WriteLine("NOT ASYNC" + Thread.CurrentContext.ContextID.ToString());
        //        Thread.Sleep(1000);
        //    }
        //    return 1000000;
        //}

        //static async Task DoVoidAsync()
        //{
        //    await Task.Delay(3000);
        //    Console.WriteLine("task ended");
        //}


    }
}
