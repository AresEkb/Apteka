﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "первичная упаковка", "первичные упаковки", "упаковка, которая соприкасается с продукцией (потребительская упаковка)")]
    [Category("CodeLists/Medicines")]
    public class PrimaryPackaging : INamedEntity
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "название")]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
