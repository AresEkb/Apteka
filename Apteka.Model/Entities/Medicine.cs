using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "лекарственное средство", "лекарственные средства", "")]
    [Category("CodeLists/Medicines")]
    public class Medicine
    {
        public Medicine()
        {
            DosageForms = new List<MedicineDosageForm>();
        }

        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "торговое наименование")]
        [UniqueIndex]
        [MaxLength(200)]
        public string TradeName { get; set; }

        [DataElement("ru", "МНН", "международное непатентованное наименование")]
        [MaxLength(300)]
        public string Inn { get; set; }

        public PharmacotherapeuticGroup PharmacotherapeuticGroup { get; set; }

        [DataElement("ru", "код АТХ")]
        public AtcGroup AtcCode { get; set; }

        [DataElement("ru", "формы выпуска")]
        public virtual ICollection<MedicineDosageForm> DosageForms { get; set; }

        public override string ToString()
        {
            return $"Лекарственное средство ({Inn})";
        }
    }
}
