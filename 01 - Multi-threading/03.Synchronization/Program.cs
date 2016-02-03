using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _03.Synchronization
{
    class Program
    {
        static Mutex m;
        static AutoResetEvent are;
        static AutoResetEvent are1;
        static ManualResetEvent mre;
        static Barrier b;
        static void Main(string[] args)
        {
            //ARESample();
            mre = new ManualResetEvent(false);
            mre.Reset();
            mre.Set();
            var b = mre.WaitOne(TimeSpan.FromSeconds(5));

        }

        private static void MRESample()
        {
            mre = new ManualResetEvent(false);
            Task.Factory.StartNew(Action1);
            Task.Factory.StartNew(Action2);
            Task.Factory.StartNew(Action3);
            Thread.Sleep(3000);
            mre.Set();
            Thread.Sleep(1000);
            mre.Reset();
            Console.WriteLine("mre reset");
            Task.Factory.StartNew(Action4);
            Console.ReadKey();
        }
        
        static void Action1()
        {
            //AutoResetEvent.WaitAny(new WaitHandle[] { are, are1 });
            //mre.WaitOne();
            are.WaitOne();
            //m.WaitOne();
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("111");
                Thread.Sleep(1000);
            }
            //b.SignalAndWait();
            are.Set();
            //m.ReleaseMutex();
        }
        static void Action2()
        {
            //mre.WaitOne();
            are.WaitOne();
            //m.WaitOne();
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("222");
                Thread.Sleep(1000);
            }
            //b.SignalAndWait();
            are.Set();
            //m.ReleaseMutex();
        }
        static void Action3()
        {
            //mre.WaitOne();
            are.WaitOne();
            //m.WaitOne();
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("333");
                Thread.Sleep(1000);
            }
            //b.SignalAndWait();
            are.Set();
            //m.ReleaseMutex();
        }
        static void Action4()
        {
            //mre.WaitOne();
            are.WaitOne();
            //m.WaitOne();
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("444");
                Thread.Sleep(1000);
            }
            //b.SignalAndWait();
            are.Set();
            //m.ReleaseMutex();
        }

        private static void SpinLockSample()
        {
            StringBuilder builder = new StringBuilder();
            SpinLock spin = new SpinLock();

            Action<string> a = new Action<string>((msg) =>
            {
                var obj = false;
                try
                {
                    spin.Enter(ref obj);
                    for (var i = 0; i < 50; i++)
                    {
                        Thread.Sleep(20);
                        builder.AppendLine(msg);
                    }
                }
                finally
                {
                    if (obj)
                        spin.Exit();
                }

            });
            Parallel.Invoke(() => a("1111"), () => a("2222"), () => a("3333"), () => a("4444"));
            Console.WriteLine(builder.ToString());
            Console.ReadKey();

        }
        private static void MutexSample()
        {
            StringBuilder builder = new StringBuilder();
            Mutex m = new Mutex();

            Action<string> a = new Action<string>((msg) =>
            {
                m.WaitOne();
                for (var i = 0; i < 50; i++)
                {
                    Thread.Sleep(20);
                    builder.AppendLine(msg);
                }
                m.ReleaseMutex();
            });
            Parallel.Invoke(() => a("1111"), () => a("2222"), () => a("3333"), () => a("4444"));
            Console.WriteLine(builder.ToString());
            Console.ReadKey();
        }
        private static void MutexSample1()
        {
            m = new Mutex();
            Task.Factory.StartNew(Action1);
            Task.Factory.StartNew(Action2);
            Task.Factory.StartNew(Action3);
        }
        private static void ARESample()
        {
            are = new AutoResetEvent(true);
            are.Reset();
            Task.Factory.StartNew(Action1);
            Task.Factory.StartNew(Action2);
            //Task.Factory.StartNew(Action3);
            //Task.Factory.StartNew(Action4);
            Thread.Sleep(2500);
            are.Set();
            Console.ReadKey();
        }
    }
}
