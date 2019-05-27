using System;
using System.Xml;
using System.Xml.Serialization;

namespace Apteka.Model.Dto
{
    public class InvoiceXmlHeader
    {
        [XmlElement("ТипДок")]
        public string TipDok { get; set; }

        [XmlElement("НомерДок")]
        public string NomerDok { get; set; }

        [XmlIgnore]
        public DateTime? DataDok { get; set; }

        [XmlElement("ДатаДок")]
        public string DataDokStr
        {
            get => InvoiceXml.PrintDateTime(DataDok);
            set => DataDok = InvoiceXml.ParseDateTime(value);
        }

        [XmlIgnore]
        public DateTime? DataOtgruzki { get; set; }

        [XmlElement("ДатаОтгрузки")]
        public string DataOtgruzkiStr
        {
            get => InvoiceXml.PrintDateTime(DataOtgruzki);
            set => DataOtgruzki = InvoiceXml.ParseDateTime(value);
        }

        [XmlElement("Поставщик")]
        public string Postavshhik { get; set; }

        [XmlElement("Получатель")]
        public string Poluchatel { get; set; }

        [XmlElement("Грузополучатель")]
        public string Gruzopoluchatel { get; set; }

        [XmlElement("УсловияОплаты")]
        public string UslovijaOplaty { get; set; }

        [XmlElement("ТоварнаяГруппа")]
        public string TovarnajaGruppa { get; set; }

        [XmlElement("Позиций")]
        public int Pozicij { get; set; }

        [XmlElement("СуммаОпт")]
        public decimal SummaOpt { get; set; }

        [XmlElement("СуммаНДС")]
        public decimal SummaNDS { get; set; }

        [XmlElement("СуммаОптВклНДС")]
        public decimal SummaOptVklNDS { get; set; }

        [XmlElement("Примечание")]
        public string Primechanie { get; set; }

        [XmlElement("РеквизитыПоставщика")]
        public InvoiceXmlSupplier RekvizityPostavshhika { get; set; }
    }
}
