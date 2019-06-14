using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities.Place
{
    [DataElement("ru", "страна", "страны", "")]
    [Category("CodeLists")]
    public class Country : EntityBase, INamedEntity
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "код")]
        //[Required]
        //[AlternateKey]
        [MinLength(2), MaxLength(2), NonUnicode]
        public string Code { get; set; }

        [DataElement("ru", "название")]
        [Required]
        [UniqueIndex]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
