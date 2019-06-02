﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "страна", "страна", "страны", "")]
    [Category("CodeLists")]
    public class Country : IEntity
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "код", "код", "")]
        //[Required]
        //[AlternateKey]
        [MinLength(2), MaxLength(2), NonUnicode]
        public string Code { get; set; }

        [DataElement("ru", "название", "название", "")]
        [Required]
        [UniqueIndex]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
