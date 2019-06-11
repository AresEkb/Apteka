using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "форма выпуска", "формы выпуска", "")]
    [Category("CodeLists/Medicines")]
    public class MedicineDosageForm
    {
        public MedicineDosageForm()
        {
            Organizations = new List<MedicineDosageFormOrganization>();
            PriceLimits = new List<MedicinePriceLimit>();
        }

        [Key, Browsable(false)]
        public int Id { get; private set; }

        [Required]
        public virtual Medicine Medicine { get; set; }

        [DataElement("ru", "лекарственная форма")]
        public virtual DosageForm DosageForm { get; set; }

        [DataElement("ru", "дозировка")]
        [DecimalPrecision(18, 6)]
        public decimal? DosageMeasure { get; set; }

        [DataElement("ru", "единица измерения дозировки")]
        public virtual MeasurementUnit DosageMeasurementUnit { get; set; }

        [DataElement("ru", "мера лекарственных форм")]
        [DecimalPrecision(18, 6)]
        public decimal? DosageFormMeasure { get; set; }

        [DataElement("ru", "единица измерения лекарственных форм")]
        public virtual MeasurementUnit DosageFormMeasurementUnit { get; set; }

        [DataElement("ru", "первичная упаковка")]
        public virtual PrimaryPackaging PrimaryPackaging { get; set; }

        [DataElement("ru", "количество первичных упаковок")]
        public int? PrimaryPackagingCount { get; set; }

        [DataElement("ru", "вторичная упаковка")]
        public virtual SecondaryPackaging SecondaryPackaging { get; set; }

        [DataElement("ru", "количество вторичных упаковок")]
        public int? SecondaryPackagingCount { get; set; }

        [DataElement("ru", "вторичная упаковка 2")]
        public virtual SecondaryPackaging SecondaryPackaging2 { get; set; }

        [DataElement("ru", "количество вторичных упаковок 2")]
        public int? PrimaryPackaging2Count { get; set; }

        [DataElement("ru", "количество в потреб. упаковке")]
        public int? TotalCount { get; set; }

        [DataElement("ru", "номер регистрационного удостоверения")]
        [MaxLength(20)]
        public string RegistrationCertificateNumber { get; set; }

        [DataElement("ru", "дата регистрации")]
        [DataType(DataType.Date)]
        public DateTime RegistrationCertificateIssueDate { get; set; }

        [DataElement("ru", "дата окончания действия регистрационного удостоверения")]
        [DataType(DataType.Date)]
        public DateTime? RegistrationCertificateExpiryDate { get; set; }

        [DataElement("ru", "дата аннулирования регистрационного удостоверения")]
        [DataType(DataType.Date)]
        public DateTime? RegistrationCertificateCancellationDate { get; set; }

        [DataElement("ru", "получатель сертификата", "юридическое лицо, на имя которого выдано регистрационное удостоверение")]
        public virtual Organization CertificateRecipient { get; set; }

        //[DataElement("ru", "страна получателя сертификата")]
        //public Country CertificateRecipientCountry { get; set; }

        //[DataElement("ru", "производитель")]
        //public Organization Manufacturer { get; set; }

        [DataElement("ru", "участники цепочки поставок")]
        public virtual ICollection<MedicineDosageFormOrganization> Organizations { get; set; }

        [DataElement("ru", "нормативная документация")]
        [MaxLength(300)]
        public string NormativeDocument { get; set; }

        [DataElement("ru", "EAN13")]
        [UniqueIndex]
        [MinLength(13), MaxLength(13), NonUnicode]
        public string Ean13 { get; set; }

        public virtual ICollection<MedicinePriceLimit> PriceLimits { get; }

        public override string ToString()
        {
            return $"Лекарственная форма препарата (EAN13={Ean13})";
        }
    }
}
