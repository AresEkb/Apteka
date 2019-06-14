using System;

using Apteka.Model.Dtos.Base;
using Apteka.Model.Entities.Base;
using Apteka.Model.Extensions;

namespace Apteka.Model.Dtos
{
    public class StateMedicineRegistryItem : IRecord, IHashable
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
            RegistrationCertificateIssueDate.HasValue;

        public long Hash
        {
            get
            {
                string str = RegistrationCertificateNumber +
                    CertificateRecipient + CertificateRecipientCountry +
                    TradeName + Inn + DosageForms + ManufactureStages +
                    Package + NormativeDocument;
                // Seems to be good enough. I didn't found any collisions
                return str.KnuthHash();
            }
        }
    }
}
