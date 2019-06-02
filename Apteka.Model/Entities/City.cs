using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "город", "город", "города", "")]
    [Category("CodeLists")]
    public class City : IEntity
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [MaxLength(10), NonUnicode]
        [UniqueIndex]
        public string Code { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }
    }
}
