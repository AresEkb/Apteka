using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace Apteka.Model.Dto
{
    // В схеме данных все поля называются нормально
    // Здесь же используется транслит потому что
    // 1) желательно, чтобы структура была максимально приближена к структуре XML документа
    // 2) просто нет смысла придумывать другие имена, всё равно это промежуточная структура

    // Даты имеют кастомный тип вместо xs:date или xs:dateTime,
    // поэтому нужны танцы с бубнами, чтобы их парсить

    // DataContract поддерживает только элементы и не поддерживает атрибуты,
    // поэтому его нельзя использовать
    [XmlRoot("Документ")]
    public class InvoiceXml
    {
        private const string DATE_FORMAT = "dd.MM.yyyy";
        private const string DATE_TIME_FORMAT = "dd.MM.yyyy H:mm:ss";
        private static readonly string[] DATE_TIME_FORMATS = { "dd.MM.yyyy HH:mm:ss", "dd.MM.yyyy H:mm:ss" };

        [XmlAttribute("Идентификатор")]
        public Guid Identifikator { get; set; }

        [XmlElement("ЗаголовокДокумента", Order = 1)]
        public InvoiceXmlHeader ZagolovokDokumenta { get; set; }

        [XmlArray("ТоварныеПозиции", Order = 2)]
        [XmlArrayItem("ТоварнаяПозиция")]
        public List<InvoiceXmlItem> TovarnyePozicii;

        internal static DateTime? ParseDateTime(string str)
        {
            return String.IsNullOrWhiteSpace(str) ? (DateTime?)null
                : DateTime.ParseExact(str, DATE_TIME_FORMATS, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        internal static string PrintDateTime(DateTime? dt)
        {
            return dt?.ToString(DATE_TIME_FORMAT);
        }

        internal static DateTime? ParseDate(string str)
        {
            return String.IsNullOrWhiteSpace(str) ? (DateTime?)null
                : DateTime.ParseExact(str, DATE_FORMAT, CultureInfo.InvariantCulture);
        }

        internal static string PrintDate(DateTime? dt)
        {
            return dt?.ToString(DATE_FORMAT);
        }
    }
}
