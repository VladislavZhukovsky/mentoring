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
            using (var queue = new MessageQueue(@"./private$/myqueue"))
            {
            }
        }
    }
}
