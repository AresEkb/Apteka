﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "анатомо-терапевтическо-химическая группа", "АТХ", "")]
    [Category("CodeLists/Medicines")]
    public class AtcGroup : EntityBase, INamedEntity
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        public virtual AtcGroup Parent { get; set; }

        [DataElement("ru", "код")]
        [MaxLength(15)]
        public string Code { get; set; }

        [DataElement("ru", "название")]
        [Required]
        [UniqueIndex]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
