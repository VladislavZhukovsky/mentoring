using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace XmlVerify
{
    class Program
    {
        static XmlReaderSettings settings;

        static void Main(string[] args)
        {
            try
            {

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add("http://library.by/catalog", "books.xsd");
                settings.ValidationType = ValidationType.Schema;
                XmlReader reader = XmlReader.Create("books.xml", settings);
                XmlDocument document = new XmlDocument();
                document.Load(reader);
                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
                document.Validate(eventHandler);
                reader.Close();
            }
            catch (XmlSchemaValidationException ex)
            {
                Console.WriteLine(String.Format("{0} \nLine: {1}, position: {2}", ex.Message, ex.LineNumber, ex.LinePosition));
            }
            Console.ReadKey();
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
