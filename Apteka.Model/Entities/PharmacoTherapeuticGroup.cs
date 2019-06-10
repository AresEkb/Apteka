﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "фармакотерапевтическая группа", "фармакотерапевтические группы", "")]
    [Category("CodeLists")]
    public class PharmacotherapeuticGroup : INamedEntity
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "код")]
        [MaxLength(15)]
        public string Code { get; set; }

        [DataElement("ru", "название")]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}