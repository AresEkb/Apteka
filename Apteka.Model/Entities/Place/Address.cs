using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities.Place
{
    [DataElement("ru", "адрес", "адреса", "")]
    public class Address
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "страна")]
        public Country Country { get; set; }

        [DataElement("ru", "город")]
        public City City { get; set; }

        [DataElement("ru", "описание")]
        [MaxLength(300)]
        public string Description { get; set; }

        [DataElement("ru", "название")]
        public string Name { get => String.Join(", ", new string[]
            { Country?.Name, City?.Name, Description }
            .Where(s => !String.IsNullOrWhiteSpace(s))); }
    }
}
