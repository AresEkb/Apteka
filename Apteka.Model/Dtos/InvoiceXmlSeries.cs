using System;
using System.Xml;
using System.Xml.Serialization;

namespace Apteka.Model.Dtos
{
    public class InvoiceXmlSeries
    {
        [XmlElement("СерияТовара")]
        public string SerijaTovara { get; set; }

        [XmlElement("НомерСертиф")]
        public string NomerSertif { get; set; }

        [XmlElement("ОрганСертиф")]
        public string OrganSertif { get; set; }

        [XmlIgnore]
        public DateTime? DataVydachiSertif { get; set; }

        [XmlElement("ДатаВыдачиСертиф")]
        public string DataVydachiSertifStr
        {
            get => InvoiceXml.PrintDate(DataVydachiSertif);
            set => DataVydachiSertif = InvoiceXml.ParseDate(value);
        }

        [XmlIgnore]
        public DateTime? SrokDejstvijaSertif { get; set; }

        [XmlElement("СрокДействияСертиф")]
        public string SrokDejstvijaSertifStr
        {
            get => InvoiceXml.PrintDate(SrokDejstvijaSertif);
            set => SrokDejstvijaSertif = InvoiceXml.ParseDate(value);
        }

        [XmlIgnore]
        public DateTime? SrokGodnostiTovara { get; set; }

        [XmlElement("СрокГодностиТовара")]
        public string SrokGodnostiTovaraStr
        {
            get => InvoiceXml.PrintDate(SrokGodnostiTovara);
            set => SrokGodnostiTovara = InvoiceXml.ParseDate(value);
        }

        [XmlElement("РегНомер")]
        public string RegNomer { get; set; }

        [XmlElement("РегНомерСертиф")]
        public string RegNomerSertif { get; set; }

        [XmlIgnore]
        public DateTime? RegDataSertif { get; set; }

        [XmlElement("РегДатаСертиф")]
        public string RegDataSertifStr
        {
            get => InvoiceXml.PrintDate(RegDataSertif);
            set => RegDataSertif = InvoiceXml.ParseDate(value);
        }

        [XmlElement("РегОрганСертиф")]
        public string RegOrganSertif { get; set; }
    }
}
