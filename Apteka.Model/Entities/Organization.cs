using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;
using Apteka.Model.Entities.Place;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "организация", "организации", "")]
    [Category("CodeLists")]
    public class Organization : IEntity
    {
        public Organization()
        {
            BankAccounts = new List<BankAccount>();
        }

        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "название")]
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [DataElement("ru", "ИНН")]
        [MaxLength(20)]
        public string TaxpayerCode { get; set; }

        [DataElement("ru", "КПП")]
        [MaxLength(20)]
        public string TaxRegistrationReasonCode { get; set; }

        [DataElement("ru", "адрес")]
        [Composition]
        public Address Address { get; set; }

        [DataElement("ru", "телефон")]
        [MaxLength(20)]
        [Phone]
        public string PhoneNumber { get; set; }

        [DataElement("ru", "email")]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [DataElement("ru", "банковские счета", BriefName = "счета")]
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
    }
}
