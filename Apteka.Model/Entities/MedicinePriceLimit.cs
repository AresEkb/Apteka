using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    public class MedicinePriceLimit
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [Required]
        public virtual MedicineDosageForm DosageForm { get; set; }

        [DataElement("ru", "предельная цена руб. без НДС")]
        [DecimalPrecision(18, 2)]
        public decimal PriceLimit { get; set; }

        [DataElement("ru", "цена указана для первич. упаковки")]
        public bool PrimaryPackagePrice { get; set; }

        [DataElement("ru", "дата регистрации цены")]
        public DateTime RegistrationDate { get; set; }

        [DataElement("ru", "№ решения")]
        [MaxLength(100)]
        public string RegistrationDocNumber { get; set; }

        [DataElement("ru", "дата исключения")]
        public DateTime? ExclusionDate { get; set; }

        [DataElement("ru", "причина исключения")]
        public virtual MedicinePriceLimitExclusionReason ExclusionReason { get; set; }
    }
}
