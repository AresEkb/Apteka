using System;
using System.Text.RegularExpressions;

namespace Apteka.Model.Dtos
{
    public class StateMedicineRegistryItem
    {
        public string RegistrationCertificateNumber { get; set; }
        public DateTime? RegistrationCertificateIssueDate { get; set; }
        public DateTime? RegistrationCertificateExpiryDate { get; set; }
        public DateTime? RegistrationCertificateCancellationDate { get; set; }
        public string CertificateRecipient { get; set; }
        public string CertificateRecipientCountry { get; set; }
        public string TradeName { get; set; }
        public string Inn { get; set; }
        public string DosageForms { get; set; }
        public string ManufactureStages { get; set; }
        public string Package { get; set; }
        public string NormativeDocument { get; set; }
        public string PharmacotherapeuticGroup { get; set; }

        public bool IsOk =>
            !String.IsNullOrWhiteSpace(RegistrationCertificateNumber) &&
            RegistrationCertificateIssueDate.HasValue /*&&
            Regex.IsMatch(Package, "^[0-9]{13}") &&
            !Regex.IsMatch(Package, "^0{13}")*/;

        public long Hash
        {
            get
            {
                string str = RegistrationCertificateNumber +
                    CertificateRecipient + CertificateRecipientCountry +
                    TradeName + Inn + DosageForms + ManufactureStages +
                    Package + NormativeDocument;
                // https://stackoverflow.com/a/9545731/632199
                // Seems to be good enough. I didn't found any collisions
                ulong hashedValue = 3074457345618258791ul;
                for (int i = 0; i < str.Length; i++)
                {
                    hashedValue += str[i];
                    hashedValue *= 3074457345618258799ul;
                }
                return unchecked((long)hashedValue + long.MinValue);
            }
        }
    }
}
