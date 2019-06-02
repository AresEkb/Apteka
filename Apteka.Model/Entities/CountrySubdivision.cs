using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    public class CountrySubdivision
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [MaxLength(2), NonUnicode]
        public string Code { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }
    }
}
