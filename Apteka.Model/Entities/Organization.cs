using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Apteka.Model.Entities
{
    public class Organization
    {
        [Browsable(false)]
        public int Id { get; protected set; }

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

    public class Address
    {
        [Browsable(false)]
        public int Id { get; protected set; }

        public Country Country { get; set; }

        public City City { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
