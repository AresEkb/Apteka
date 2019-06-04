using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities.Place
{
    [DataElement("ru", "регион", "регионы", "")]
    public class CountrySubdivision
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "код")]
        [MaxLength(2), NonUnicode]
        public string Code { get; set; }

        [DataElement("ru", "название")]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
