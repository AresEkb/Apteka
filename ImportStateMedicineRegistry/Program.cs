using System;
using System.IO;
using System.Linq;
using System.Text;

using CsvHelper;
using CsvHelper.Configuration;

namespace ImportStateMedicineRegistry
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var reader = new StreamReader("grls2019-06-04-1.csv", Encoding.GetEncoding("windows-1251")))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<StateMedicineRegistryItemMap>();
                csv.Read(); // Skip first line
                var records = csv.GetRecords<StateMedicineRegistryItem>();
                foreach (var item in records.Take(10))
                {
                    Console.WriteLine(item.RegistrationCertificateIssueDate);
                }
            }
            Console.ReadKey();
        }

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
            public string ReleaseForms { get; set; }
            public string ManufactureStages { get; set; }
            public string Package { get; set; }
            public string NormativeDocument { get; set; }
            public string PharmacotherapeuticGroup { get; set; }
        }

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
                Map(m => m.ReleaseForms).Index(10);
                Map(m => m.ManufactureStages).Index(11);
                Map(m => m.Package).Index(12);
                Map(m => m.NormativeDocument).Index(13);
                Map(m => m.PharmacotherapeuticGroup).Index(14);
            }
        }

        public class StateMedicinePriceLimitRegistryItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
