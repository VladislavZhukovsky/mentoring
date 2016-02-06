using DocumentProcessor.Core.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentProcessor.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var qm = new QueueManager();
            var m = qm.ReceiveMessage();
        }
    }
}
