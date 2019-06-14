using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "лекарственная форма", "лекарственные формы", "")]
    [Category("CodeLists/Medicines")]
    public class DosageForm : EntityBase, INamedEntity
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "название")]
        [Required]
        [UniqueIndex]
        [MaxLength(300)]
        public string Name { get; set; }
    }
}
