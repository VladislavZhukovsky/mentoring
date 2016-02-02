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

            string sourceDir = Path.Combine(currentDir, "Source");
            string dstDir = Path.Combine(currentDir, "Destination");

            //Debugger.Launch();

            var fileProcessor = new FileProcessor(sourceDir, dstDir);
        }
    }
}
