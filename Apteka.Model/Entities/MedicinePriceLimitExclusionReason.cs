using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "причина исключения", "причины исключения", "")]
    [Category("CodeLists")]
    public class MedicinePriceLimitExclusionReason
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "название")]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
