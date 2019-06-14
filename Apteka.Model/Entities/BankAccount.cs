using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "банковский счет", "счета", "банковский счет организации", BriefName = "счет")]
    public class BankAccount : EntityBase
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [Required]
        public virtual Organization Organization { get; set; }

        [DataElement("ru", "название")]
        public string Name { get => CheckingAccount + " (" + BankName + ")"; }

        [DataElement("ru", "расчётный счёт", BriefName = "счёт")]
        [MaxLength(50), NonUnicode]
        public string CheckingAccount { get; set; }

        [DataElement("ru", "корреспондентский счёт", BriefName = "кор. счёт")]
        [MaxLength(50), NonUnicode]
        public string CorrespondentAccount { get; set; }

        [DataElement("ru", "БИК")]
        [MaxLength(20)]
        public string BankCode { get; set; }

        [DataElement("ru", "банк")]
        [MaxLength(200)]
        public string BankName { get; set; }

        [DataElement("ru", "отделение банка", BriefName = "отделение")]
        [MaxLength(200)]
        public string BankBranchName { get; set; }
    }
}
