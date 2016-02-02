using FileTransformer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorService
{
    class Program
    {
        static void Main(string[] args)
        {
            var transformer = new PdfProcessor();
            var files = Directory.EnumerateFiles(Path.GetDirectoryName(Path.GetFullPath(Process.GetCurrentProcess().MainModule.FileName)));
            var images = files.Where(x => Path.GetExtension(x) == ".jpg");

            transformer.CreatePdf(images, "pdf1.pdf");
        }
    }
}
