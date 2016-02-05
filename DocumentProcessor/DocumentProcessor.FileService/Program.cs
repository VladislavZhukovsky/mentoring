using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentProcessor.FileService
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDir = Path.GetDirectoryName(Path.GetFullPath(Process.GetCurrentProcess().MainModule.FileName));

            string sourceDir = @"D:\Vlad\Mentoring\DocumentProcessor\Source";
            string dstDir =    @"D:\Vlad\Mentoring\DocumentProcessor\Destination";

            var fileProcessor = new FileProcessor(sourceDir, dstDir);
        }
    }
}
