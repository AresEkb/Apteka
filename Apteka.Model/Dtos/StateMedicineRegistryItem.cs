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
    }
}
