using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DocumentProcessor.Core;
using DocumentProcessor.PdfProcessor;
using System.Text.RegularExpressions;
using DocumentProcessor.Core.Queue;

namespace DocumentProcessor.ProcessorService
{
    class Program
    {
        static void Main(string[] args)
        {
            var qm = new QueueManager();
            var m = qm.ReceiveFiles();
        }
    }
}
