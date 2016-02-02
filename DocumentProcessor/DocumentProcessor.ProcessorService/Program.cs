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

namespace DocumentProcessor.ProcessorService
{
    class Program
    {
        static void Main(string[] args)
        {
            IProcessor transformer = new DocumentProcessor.PdfProcessor.PdfProcessor();
            var files = Directory.EnumerateFiles("Source");
            var images = files.Where(x => Path.GetExtension(x) == ".jpg");

            transformer.Process(images, "Docs");
        }
    }
}
