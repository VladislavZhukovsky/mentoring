using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace XsltTransform
{
    class Program
    {
        static void Main(string[] args)
        {
            var xsl = new XslCompiledTransform(true);
            xsl.Load("books.xslt");
            xsl.Transform("books.xml", "result.html");

        }
    }
}
