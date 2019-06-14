using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "единица измерения", "единицы измерения", "")]
    [Category(@"CodeLists")]
    public class MeasurementUnit : EntityBase, INamedEntity
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "название")]
        [Required]
        [UniqueIndex]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
