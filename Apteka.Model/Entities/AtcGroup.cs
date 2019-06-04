using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "анатомо-терапевтическо-химическая группа", "АТХ", "")]
    [Category("CodeLists")]
    public class AtcGroup
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        public AtcGroup Parent { get; set; }

        [DataElement("ru", "код")]
        [MaxLength(15)]
        public string Code { get; set; }

        [DataElement("ru", "название")]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
