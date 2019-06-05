using Apteka.Model.Dtos;

using CsvHelper.Configuration;

namespace ImportStateMedicineRegistry
{
    partial class Program
    {
        public sealed class StateMedicineRegistryItemMap : ClassMap<StateMedicineRegistryItem>
        {
            public StateMedicineRegistryItemMap()
            {
                Map(m => m.RegistrationCertificateNumber).Index(0);
                Map(m => m.RegistrationCertificateIssueDate).Index(1);
                Map(m => m.RegistrationCertificateExpiryDate).Index(2);
                Map(m => m.RegistrationCertificateCancellationDate).Index(4);
                Map(m => m.CertificateRecipient).Index(5);
                Map(m => m.CertificateRecipientCountry).Index(6);
                Map(m => m.TradeName).Index(7);
                Map(m => m.Inn).Index(9);
                Map(m => m.DosageForms).Index(10);
                Map(m => m.ManufactureStages).Index(11);
                Map(m => m.Package).Index(12);
                Map(m => m.NormativeDocument).Index(13);
                Map(m => m.PharmacotherapeuticGroup).Index(14);
            }
        }
    }
}
