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
            var settings = new XsltSettings { EnableScript = true };
            xsl.Load("books.xslt", settings, null);
            xsl.Transform("books.xml", "report.html");

        }
    }
}
