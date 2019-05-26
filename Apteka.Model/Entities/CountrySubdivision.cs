using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Apteka.Model.Entities
{
    public class CountrySubdivision
    {
        [Browsable(false)]
        public int Id { get; protected set; }

        [MaxLength(2), NonUnicode]
        public string Code { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }
    }
}
