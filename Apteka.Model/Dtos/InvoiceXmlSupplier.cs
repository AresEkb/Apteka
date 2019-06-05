using System.Xml;
using System.Xml.Serialization;

namespace Apteka.Model.Dtos
{
    public class InvoiceXmlSupplier
    {
        [XmlElement("Адрес")]
        public string Adres { get; set; }

        [XmlElement("ИНН")]
        public string INN { get; set; }

        [XmlElement("Телефоны")]
        public string Telefony { get; set; }

        [XmlElement("РасчетныйСчет")]
        public string RaschetnyjSchet { get; set; }

        [XmlElement("Город")]
        public string Gorod { get; set; }

        [XmlElement("Банк")]
        public string Bank { get; set; }

        [XmlElement("ОтделениеБанка")]
        public string OtdelenieBanka { get; set; }

        [XmlElement("БИК")]
        public string BIK { get; set; }

        [XmlElement("КорСчет")]
        public string KorSchet { get; set; }

        [XmlElement("ЭлПочта")]
        public string JelPochta { get; set; }
    }
}
