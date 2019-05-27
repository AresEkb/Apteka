using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "организация", "организация", "организации", "")]
    [Category("CodeLists")]
    public class Organization
    {
        public Organization()
        {
            BankAccounts = new List<BankAccount>();
        }

        [Browsable(false)]
        public int Id { get; protected set; }

        [DataElement("ru", "название", "название", "")]
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [DataElement("ru", "ИНН", "ИНН", "")]
        [MaxLength(20)]
        public string TaxpayerCode { get; set; }

        [DataElement("ru", "КПП", "КПП", "")]
        [MaxLength(20)]
        public string TaxRegistrationReasonCode { get; set; }

        [DataElement("ru", "адрес", "адрес", "")]
        public Address Address { get; set; }

        [DataElement("ru", "телефон", "телефон", "")]
        [MaxLength(20)]
        [Phone]
        public string PhoneNumber { get; set; }

        [DataElement("ru", "email", "email", "")]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [DataElement("ru", "банковские счета", "счета", "")]
        [Composition]
        public virtual List<BankAccount> BankAccounts { get; set; }
    }
}
