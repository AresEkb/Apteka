using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "организация", "организация", "организации", "")]
    [Category("CodeLists/Orgs")]
    public class Organization
    {
        [Browsable(false)]
        public int Id { get; protected set; }

        [DataElement("ru", "название", "название", "")]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string TaxpayerCode { get; set; }

        [MaxLength(20)]
        public string TaxRegistrationReasonCode { get; set; }

        public Address Address { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }
    }
}
