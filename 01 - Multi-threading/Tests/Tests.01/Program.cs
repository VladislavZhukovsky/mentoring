using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests._01
{
    class Program
    {
        static object state;
        static void Main(string[] args)
        {
            state = 5;
            var t1 = new Thread(M1);
            var t2 = new Thread(M2);
            t1.Start(state);

            t2.Start(state);

        }

        static void M1(object obj)
        {
            lock(obj)
            {
                for (var i = 0; i < 10; i++)
                {
                    Console.WriteLine("111");
                    Thread.Sleep(300);
                }
            }
        }

        static void M2(object obj)
        {
            lock (obj)
            {
                for (var i = 0; i < 10; i++)
                {
                    Console.WriteLine("222");
                    Thread.Sleep(300);
                }
            }
        }

        #region Q3
        //answer: No exception
        //static void Q3()
        //{
        //    try
        //    {
        //        Task.Factory.StartNew(Do);
        //        Console.WriteLine("No exception");
        //    }
        //    catch (AggregateException ex)
        //    {
        //        Console.WriteLine("Ex handled");
        //    }
        //}

        //static void Do()
        //{
        //    throw new Exception();
        //}

        #endregion

        #region Q4
        //answer: 5 3 16
        //static void Q4()
        //{
        //    var someValue = 5;
        //    var someOtherValue = 3;

        //    var t = Task.Factory.StartNew<int>(Method1, someValue)
        //    .ContinueWith<int>(Method2, someOtherValue)
        //    .ContinueWith(Method3);

        //    t.Wait();
        //    Console.ReadKey();
        //}

        //static int Method1(object state)
        //{
        //    Console.WriteLine(state);
        //    return (int)Math.Pow((int)state, 2);
        //}

        //static int Method2(Task<int> task, object state)
        //{
        //    Console.WriteLine(state);
        //    return (int)(task.Result - Math.Pow((int)state, 2));
        //}

        //static void Method3(Task<int> task)
        //{
        //    Console.WriteLine(task.Result);
        //}

        #endregion

        #region Q5
        //answer: 3
        //static void Q5()
        //{
        //    var task = Task.Factory.StartNew<int>(Method1, null)
        //    .ContinueWith<int>(Method2);
        //    Console.WriteLine(task.Result); // #3 
        //}

        //static int Method1(object state)
        //{
        //    Console.WriteLine("task1");
        //    return (int)Math.Pow((int)state, 2); // #1 
        //}

        //static int Method2(Task<int> task)
        //{
        //    Console.WriteLine("task2");
        //    return 2 / task.Result; // #2 
        //}

        #endregion

        #region Q6
        //answer: 3
        //static int Method1(object state)
        //{
        //    try
        //    {
        //        return (int)Math.Pow((int)state, 2);
        //    }
        //    catch
        //    {
        //        return 2;
        //    }
        //}

        //static int Method2(Task<int> task)
        //{
        //    try
        //    {
        //        return 2 / task.Result;
        //    }
        //    catch
        //    {
        //        return 3;
        //    }
        //}

        //static void Main()
        //{
        //    var task = Task.Factory.StartNew<int>(Method1, 0)
        //    .ContinueWith<int>(Method2);

        //    Console.WriteLine(task.Result);
        //    Console.ReadKey();
        //}

        #endregion

    }
}
