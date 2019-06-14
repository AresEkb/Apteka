using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities.Place
{
    [DataElement("ru", "адрес", "адреса", "")]
    public class Address : EntityBase
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "страна")]
        public virtual Country Country { get; set; }

        [DataElement("ru", "город")]
        public virtual City City { get; set; }

        [DataElement("ru", "описание")]
        [MaxLength(300)]
        public string Description { get; set; }

        [DataElement("ru", "название")]
        public string Name { get => String.Join(", ", new string[]
            { Country?.Name, City?.Name, Description }
            .Where(s => !String.IsNullOrWhiteSpace(s))); }
    }
}
