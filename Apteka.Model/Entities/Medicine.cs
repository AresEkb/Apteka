﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "лекарственное средство", "лекарственные средства", "")]
    [DefaultProperty(nameof(TradeName))]
    [Category("CodeLists/Medicines")]
    public class Medicine : EntityBase
    {
        public Medicine()
        {
            DosageForms = new List<MedicineDosageForm>();
        }

        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "торговое наименование")]
        [Required]
        [UniqueIndex]
        [MaxLength(300)]
        public string TradeName { get; set; }

        [DataElement("ru", "МНН", "международное непатентованное наименование")]
        [MaxLength(700)]
        public string Inn { get; set; }

        public virtual PharmacotherapeuticGroup PharmacotherapeuticGroup { get; set; }

        [DataElement("ru", "код АТХ")]
        public virtual AtcGroup AtcCode { get; set; }

        [DataElement("ru", "формы выпуска")]
        public virtual ICollection<MedicineDosageForm> DosageForms { get; set; }
    }
}
