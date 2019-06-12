using System;
using System.Globalization;

using Apteka.Model.Dtos.Base;
using Apteka.Model.Extensions;

namespace Apteka.Model.Dtos
{
    public class StateMedicinePriceRegistryItem : IRecord, IHashable<long>
    {
        public string Inn { get; set; }
        public string TradeName { get; set; }
        public string DosageForms { get; set; }
        public string Organizations { get; set; }
        public string AtcCode { get; set; }
        public int? TotalCount { get; set; }
        public string PriceStr { get; set; }
        public string PrimaryPackagingPrice { get; set; }
        public string RegistrationCertificateNumber { get; set; }
        public string PriceRegistrationDateNumber { get; set; }
        public string Ean13 { get; set; }

        public DateTime? PriceRegistrationDate
        {
            get
            {
                if (String.IsNullOrWhiteSpace(PriceRegistrationDateNumber) ||
                    PriceRegistrationDateNumber.Length < 10)
                {
                    return null;
                }
                if (DateTime.TryParseExact(PriceRegistrationDateNumber.Substring(0, 10),
                    "dd.MM.yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime date))
                {
                    return date;
                }
                return null;
            }
        }

        public string PriceRegistrationNumber =>
            PriceRegistrationDateNumber.Substring(10).Trim(' ', '\n', '(', ')');

        public decimal? Price =>
            decimal.TryParse(PriceStr.Replace(" ", "").Replace("\n", "").Replace(',', '.'), out decimal result)
                ? result : (decimal?)null;

        public bool IsOk =>
            !String.IsNullOrWhiteSpace(TradeName) &&
            !String.IsNullOrWhiteSpace(Ean13) &&
            Ean13.Length == 13 &&
            Ean13 != "0000000000000" &&
            PriceRegistrationDate.HasValue &&
            Price.HasValue;

        public long Hash
        {
            get
            {
                string str = Inn + TradeName +
                    DosageForms + Organizations +
                    RegistrationCertificateNumber +
                    PriceRegistrationDateNumber +
                    Ean13;
                return str.KnuthHash();
            }
        }
    }
}
