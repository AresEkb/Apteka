using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "промежуточная упаковка", "промежуточные упаковки", "упаковка, в которую может быть помещена первичная упаковка с целью дополнительной защиты лекарственного препарата или исходя из особенностей применения лекарственного препарата")]
    [Category("CodeLists/Medicines")]
    public class IntermediatePackaging : INamedEntity
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "название")]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
