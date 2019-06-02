using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities.Place
{
    [DataElement("ru", "город", "город", "города", "")]
    [Category("CodeLists")]
    public class City : IEntity
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "код", "код", "")]
        [MaxLength(10), NonUnicode]
        [UniqueIndex]
        public string Code { get; set; }

        [DataElement("ru", "название", "название", "")]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
