using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

using Apteka.Model.Dto;
using Apteka.Model.Entities;
using Apteka.Model.Mappers;
using Apteka.Module.Factories;

using DevExpress.ExpressApp;

namespace Apteka.Module.Ixporters
{
    public class InvoiceXmlIxporter
    {
        public static void Import(Stream xml, IObjectSpace os, CollectionSourceBase collection)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(InvoiceXml));
                var invoiceXml = (InvoiceXml)serializer.Deserialize(xml);
                var mapper = new InvoiceXmlMapper(new ObjectSpaceEntityFactory(os));
                var invoice = mapper.Map(invoiceXml);
                os.CommitChanges();
                collection.Add(invoice);
            }
            catch (Exception ex)
            {
                os.Rollback();
                throw ex;
            }
        }

        public static void Export(Stream xml, IObjectSpace os, Invoice invoice)
        {
            using (var writer = new StreamWriter(xml, Encoding.GetEncoding("windows-1251")))
            {
                var serializer = new XmlSerializer(typeof(InvoiceXml));
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                var mapper = new InvoiceXmlMapper(new ObjectSpaceEntityFactory(os));
                var invoiceXml = mapper.Map(invoice);
                serializer.Serialize(writer, invoiceXml, ns);
            }
        }
    }
}
