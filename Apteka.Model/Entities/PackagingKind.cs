using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "вид упаковки", "виды упаковок", "")]
    [Category("CodeLists/Medicines")]
    public class PackagingKind : EntityBase, INamedEntity
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "название")]
        [Required]
        [UniqueIndex]
        [MaxLength(200)]
        public string Name { get; set; }

        [DataElement("ru", "первичная упаковка", "упаковка, которая соприкасается с продукцией (потребительская упаковка)")]
        public bool IsPrimary { get; set; }

        [DataElement("ru", "промежуточная упаковка", "упаковка, в которую может быть помещена первичная упаковка с целью дополнительной защиты лекарственного препарата или исходя из особенностей применения лекарственного препарата")]
        public bool IsIntermediate { get; set; }

        [DataElement("ru", "вторичная упаковка", "упаковка, которая объединяет несколько первичных упаковок")]
        public bool IsSecondary { get; set; }
    }
}
