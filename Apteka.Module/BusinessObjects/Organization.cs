using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using DevExpress.Persistent.Base;

namespace Apteka.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Organization
    {
        [Browsable(false)]
        public int Id { get; protected set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(20)]
        public string TaxpayerCode { get; set; }

        [MaxLength(20)]
        public string TaxRegistrationReasonCode { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }
    }
}
