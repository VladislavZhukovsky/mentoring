using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace MyQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            TransQueue();
        }

        static void PrivateQueue()
        {
            MessageQueue queue;
            string name = @".\Private$\myqueue";
            if (MessageQueue.Exists(name))
            {
                queue = new MessageQueue(name);
            }
            else
            {
                queue = MessageQueue.Create(name);
            }
            using (queue)
            {
                queue.Formatter = new BinaryMessageFormatter();
                queue.Send(new Foo());
                var msg = queue.Peek();
                try
                {
                    Console.WriteLine(msg.Body.ToString());
                    queue.Receive();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
            Console.ReadKey();
            //MessageQueue.Delete(name);
        }

        static void PublicQueue()
        {
            MessageQueue queue;
            string name = @".\mypublicqueue";
            if (MessageQueue.Exists(name))
            {
                queue = new MessageQueue(name);
            }
            else
            {
                queue = MessageQueue.Create(name);
            }
        }

        static void TransQueue()
        {
            MessageQueue queue;
            string name = @".\Private$\myqueue";
            if (MessageQueue.Exists(name))
            {
                queue = new MessageQueue(name);
            }
            else
            {
                queue = MessageQueue.Create(name, true);
            }
            using (queue)
            {
                using (var trans = new MessageQueueTransaction())
                {
                    trans.Begin();
                    queue.Receive(trans);
                    //queue.Send("msg4");
                    trans.Commit();
                }
            }
            
        }
    }

    [Serializable]
    public class Foo
    {
        public int x;

        public Foo()
        {
            x = 4;
        }

        public override string ToString()
        {
            return x.ToString();
        }
    }
}
