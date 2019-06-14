using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "участник цепочки поставок", "участники цепочек поставок", "")]
    public class MedicineDosageFormOrganization : EntityBase, IEntity
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [Required]
        [UniqueIndex("MedicineDosageForm_Organization_Role", 0)]
        public virtual MedicineDosageForm MedicineDosageForm { get; set; }

        [DataElement("ru", "роль")]
        //[Required]
        [UniqueIndex("MedicineDosageForm_Organization_Role", 2)]
        public virtual OrganizationRole Role { get; set; }

        [DataElement("ru", "организация")]
        // Тут нужна другая аннотация, иначе EF навешивает каскадное удаление
        //[Required]
        [UniqueIndex("MedicineDosageForm_Organization_Role", 1)]
        public virtual Organization Organization { get; set; }
    }
}
