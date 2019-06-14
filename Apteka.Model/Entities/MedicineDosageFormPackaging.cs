using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    public class MedicineDosageFormPackaging
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [Required]
        [UniqueIndex]
        public virtual MedicineDosageForm MedicineDosageForm { get; set; }

        [DataElement("ru", "порядковый номер", BriefName = "№")]
        public byte Order { get; set; }

        [DataElement("ru", "вид")]
        public virtual PackagingKind Kind { get; set; }

        [DataElement("ru", "мера")]
        [DecimalPrecision(18, 6)]
        public decimal? Measure { get; set; }

        [DataElement("ru", "единица измерения")]
        public virtual MeasurementUnit MeasurementUnit { get; set; }

        [DataElement("ru", "количество")]
        public int? Count { get; set; }

        [DataElement("ru", "примечание")]
        public string Note { get; set; }
    }
}
