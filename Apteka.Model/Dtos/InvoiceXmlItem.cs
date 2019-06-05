using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Apteka.Model.Dtos
{
    public class InvoiceXmlItem
    {
        [XmlElement("КодТовара")]
        public string KodTovara { get; set; }

        [XmlElement("Товар")]
        public string Tovar { get; set; }

        [XmlElement("Изготовитель")]
        public string Izgotovitel { get; set; }

        [XmlElement("СтранаИзготовителя")]
        public string StranaIzgotovitelja { get; set; }

        [XmlElement("Количество")]
        public int Kolichestvo { get; set; }

        [XmlElement("ЦенаИзг")]
        public decimal CenaIzg { get; set; }

        [XmlElement("ЦенаГР")]
        public decimal CenaGR { get; set; }

        [XmlElement("НаценОпт")]
        public decimal NacenOpt { get; set; }

        [XmlElement("ЦенаОпт")]
        public decimal CenaOpt { get; set; }

        [XmlElement("СуммаОпт")]
        public decimal SummaOpt { get; set; }

        [XmlElement("СтавкаНДС")]
        public decimal StavkaNDS { get; set; }

        [XmlElement("СуммаНДС")]
        public decimal SummaNDS { get; set; }

        [XmlElement("СуммаОптВклНДС")]
        public decimal SummaOptVklNDS { get; set; }

        [XmlElement("ЕАН13")]
        public string EAN13 { get; set; }

        [XmlElement("ГТД")]
        public string GTD { get; set; }

        [XmlArray("Серии")]
        [XmlArrayItem("Серия")]
        public List<InvoiceXmlSeries> Serii { get; set; }
    }
}
