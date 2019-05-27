using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "банковский счет", "счет", "счета", "банковский счет организации")]
    public class BankAccount
    {
        [Browsable(false)]
        public int Id { get; protected set; }

        [Required]
        public virtual Organization Organization { get; set; }

        public string Name { get => CheckingAccount + " (" + BankName + ")"; }

        [DataElement("ru", "расчётный счёт", "счёт", "")]
        [MaxLength(50), NonUnicode]
        public string CheckingAccount { get; set; }

        [DataElement("ru", "корреспондентский счёт", "кор. счёт", "")]
        [MaxLength(50), NonUnicode]
        public string CorrespondentAccount { get; set; }

        [DataElement("ru", "БИК", "БИК", "")]
        [MaxLength(20)]
        public string BankCode { get; set; }

        [DataElement("ru", "банк", "банк", "")]
        [MaxLength(200)]
        public string BankName { get; set; }

        [DataElement("ru", "отделение банка", "отделение", "")]
        [MaxLength(200)]
        public string BankBranchName { get; set; }
    }
}
