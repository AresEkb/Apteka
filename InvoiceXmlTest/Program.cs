using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

using Apteka.Model.Dto;

namespace InvoiceXmlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var serializer = new XmlSerializer(typeof(InvoiceXml));
            using (var reader = new FileStream("primer.xml", FileMode.Open))
            using (var writer = new StreamWriter("primer2.xml", false, Encoding.GetEncoding("windows-1251")))
            {
                var invoice = (InvoiceXml)serializer.Deserialize(reader);

                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(writer, invoice, ns);

                Console.WriteLine(invoice);
            }

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
