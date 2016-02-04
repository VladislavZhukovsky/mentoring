using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DocumentProcessor.Core;
using System.Text.RegularExpressions;
using DocumentProcessor.Core.Queue;

namespace DocumentProcessor.ProcessorService
{
    class Program
    {
        static void Main(string[] args)
        {
            var workingFolder = @"D:\Vlad\Mentoring\DocumentProcessor\Destination";
            var documentFolder = @"D:\Vlad\Mentoring\DocumentProcessor\Docs";
            var processor = new DocumentProcessor(workingFolder, documentFolder);
        }
    }
}
