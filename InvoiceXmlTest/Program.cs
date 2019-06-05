using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using Apteka.Model.Dtos;

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
            //using (var writer = new CustomXmlTextWriter("primer3.xml", Encoding.GetEncoding("windows-1251")))
            using (var writer = new StreamWriter("primer2.xml", false, Encoding.GetEncoding("windows-1251")))
            {
                //writer.Formatting = Formatting.Indented;
                //writer.Indentation = 2;
                //writer.IndentChar = ' ';
                var invoice = (InvoiceXml)serializer.Deserialize(reader);

                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(writer, invoice, ns);

                Console.WriteLine(invoice);
            }

            //Console.WriteLine("Press any key...");
            //Console.ReadKey();
        }
    }

    //public class CustomXmlTextWriter : XmlTextWriter
    //{
    //    public CustomXmlTextWriter(TextWriter w) : base(w)
    //    {
    //    }

    //    public CustomXmlTextWriter(Stream w, Encoding encoding) : base(w, encoding)
    //    {
    //    }

    //    public CustomXmlTextWriter(string filename, Encoding encoding) : base(filename, encoding)
    //    {
    //    }

    //    public override void WriteValue(decimal value)
    //    {
    //        base.WriteValue(value);
    //    }

    //    public override void WriteValue(DateTime value)
    //    {
    //        base.WriteValue(value);
    //    }

    //    public override void WriteValue(object value)
    //    {
    //        base.WriteValue(value);
    //    }

    //    public override void WriteRaw(string data)
    //    {
    //        base.WriteRaw(data);
    //    }
    //}

    //public class CustomXmlSerializer : XmlSerializer
    //{
    //    public CustomXmlSerializer(Type type) : base(type)
    //    {
    //    }

    //    protected override XmlSerializationWriter CreateWriter()
    //    {
    //        return new CustomXmlSerializationWriter();
    //    }
    //}

    //public class CustomXmlSerializationWriter : XmlSerializationWriter
    //{
    //    protected override void InitCallbacks()
    //    {
    //        AddWriteCallback(typeof(decimal), "decimal", "http://www.w3.org/2001/XMLSchema", cb);
    //    }

    //    private void cb(object o)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
