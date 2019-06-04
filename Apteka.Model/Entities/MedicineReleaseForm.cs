using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Place;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "форма выпуска", "формы выпуска", "")]
    public class MedicineReleaseForm
    {
        public MedicineReleaseForm()
        {
            PriceLimits = new List<MedicinePriceLimit>();
        }

        [Key, Browsable(false)]
        public int Id { get; private set; }

        [Required]
        public Medicine Medicine { get; set; }

        [DataElement("ru", "лекарственная форма")]
        [MaxLength(200)]
        public string DosageForm { get; set; }

        [DataElement("ru", "дозировка")]
        [MaxLength(100)]
        public string Dosage { get; set; }

        [DataElement("ru", "упаковка")]
        [MaxLength(100)]
        public string Package { get; set; }

        [DataElement("ru", "количество в потреб. упаковке")]
        public int UnitQuantity { get; set; }

        [DataElement("ru", "номер регистрационного удостоверения")]
        [MaxLength(20)]
        public string RegistrationCertificateNumber { get; set; }

        [DataElement("ru", "дата регистрации")]
        public DateTime RegistrationCertificateIssueDate { get; set; }

        [DataElement("ru", "дата окончания действия регистрационного удостоверения")]
        public DateTime? RegistrationCertificateExpiryDate { get; set; }

        [DataElement("ru", "дата аннулирования регистрационного удостоверения")]
        public DateTime? RegistrationCertificateCancellationDate { get; set; }

        [DataElement("ru", "получатель сертификата", "юридическое лицо, на имя которого выдано регистрационное удостоверение")]
        public Organization CertificateRecipient { get; set; }

        [DataElement("ru", "страна получателя сертификата")]
        public Country CertificateRecipientCountry { get; set; }

        [DataElement("ru", "производитель")]
        public Organization Manufacturer { get; set; }

        [DataElement("ru", "нормативная документация")]
        [MaxLength(200)]
        public string NormativeDocument { get; set; }

        [DataElement("ru", "EAN13")]
        [MinLength(13), MaxLength(13), NonUnicode]
        public string Ean13 { get; set; }

        public ICollection<MedicinePriceLimit> PriceLimits { get; }
    }
}
