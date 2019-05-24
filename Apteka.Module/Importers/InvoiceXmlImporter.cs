using System.IO;
using System.Xml.Serialization;

using Apteka.Model.Dto;
using Apteka.Model.Mapper;
using Apteka.Module.Mappers;

using DevExpress.ExpressApp;

namespace Apteka.Module.Importers
{
    public class InvoiceXmlImporter
    {
        public static void Import(Stream xml, IObjectSpace os, CollectionSourceBase collection)
        {
            var serializer = new XmlSerializer(typeof(InvoiceXml));
            var invoiceXml = (InvoiceXml)serializer.Deserialize(xml);
            var mapper = new InvoiceXmlMapper(new ObjectSpaceObjectFactory(os));
            var invoice = mapper.MapToModel(invoiceXml);
            // TODO: Save object
            collection.Add(invoice);
        }

        public static void Export(Stream xml, IObjectSpace os, CollectionSourceBase collection)
        {
            var serializer = new XmlSerializer(typeof(InvoiceXml));
            var invoiceXml = (InvoiceXml)serializer.Deserialize(xml);
            var mapper = new InvoiceXmlMapper(new ObjectSpaceObjectFactory(os));
            var invoice = mapper.MapToModel(invoiceXml);
            collection.Add(invoice);
        }
    }
}
