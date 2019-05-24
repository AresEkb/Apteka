using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using DevExpress.Persistent.Base;

namespace Apteka.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Country
    {
        [Browsable(false)]
        public int Id { get; protected set; }

        [MaxLength(2)]
        public string Code { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }
    }
}
