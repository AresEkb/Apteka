using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "лекарственное средство", "лекарственные средства", "")]
    public class Medicine
    {
        public Medicine()
        {
        }

        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "торговое наименование")]
        [MaxLength(200)]
        public string TradeName { get; set; }

        [DataElement("ru", "МНН", "международное непатентованное наименование")]
        [MaxLength(200)]
        public string Inn { get; set; }

        public PharmacotherapeuticGroup PharmacotherapeuticGroup { get; set; }

        [DataElement("ru", "код АТХ")]
        public AtcGroup AtcCode { get; set; }

        [DataElement("ru", "формы выпуска")]
        public ICollection<MedicineReleaseForm> ReleaseForms { get; }
    }
}
