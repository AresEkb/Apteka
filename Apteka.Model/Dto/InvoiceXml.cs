using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace Apteka.Model.Dto
{
    // В схеме данных все поля называются нормально
    // Здесь же используется транслит потому что
    // 1) желательно, чтобы структура была максимально приближена к структуре XML документа
    // 2) просто нет смысла придумывать другие имена, всё равно это промежуточная структура

    // Даты имеют кастомный тип вместо xs:date или xs:dateTime,
    // поэтому нужны танцы с бубнами, чтобы их парсить
    [XmlRoot("Документ")]
    public class InvoiceXml
    {
        private const string DATE_FORMAT = "dd.MM.yyyy";
        private const string DATE_TIME_FORMAT = "dd.MM.yyyy HH:mm:ss";
        private static readonly string[] DATE_TIME_FORMATS = { "dd.MM.yyyy HH:mm:ss", "dd.MM.yyyy H:mm:ss" };

        [XmlAttribute("Идентификатор")]
        public Guid Identifikator { get; set; }

        [XmlElement("ЗаголовокДокумента")]
        public ZagolovokDokumenta ZagolovokDokumenta { get; set; }

        [XmlArray("ТоварныеПозиции")]
        [XmlArrayItem("ТоварнаяПозиция")]
        public List<TovarnajaPozicija> TovarnyePozicii;

        public static DateTime? ParseDateTime(string str)
        {
            return String.IsNullOrWhiteSpace(str) ? (DateTime?)null
                : DateTime.ParseExact(str, DATE_TIME_FORMATS, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public static string PrintDateTime(DateTime? dt)
        {
            return dt?.ToString(DATE_TIME_FORMAT);
        }

        public static DateTime? ParseDate(string str)
        {
            return String.IsNullOrWhiteSpace(str) ? (DateTime?)null
                : DateTime.ParseExact(str, DATE_FORMAT, CultureInfo.InvariantCulture);
        }

        public static string PrintDate(DateTime? dt)
        {
            return dt?.ToString(DATE_FORMAT);
        }
    }

    public class ZagolovokDokumenta
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
        public RekvizityPostavshhika RekvizityPostavshhika { get; set; }
    }

    public class RekvizityPostavshhika
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

    public class TovarnajaPozicija
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
        public List<Serija> Serii { get; set; }
    }

    public class Serija
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
