using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Apteka.Model.Entities
{
    public class ProductSeries
    {
        [Browsable(false)]
        public int Id { get; protected set; }

        [MaxLength(20)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string CertificateCode { get; set; }

        [MaxLength(100)]
        public string CertificateAuthority { get; set; }

        public DateTime? CertificateIssueDate { get; set; }

        public DateTime? CertificateExpireDate { get; set; }

        public DateTime? ShelfLifeDate { get; set; }

        [MaxLength(50)]
        public string RegionalCertificateCode { get; set; }

        [MaxLength(100)]
        public string RegionalCertificateAuthority { get; set; }

        public DateTime? RegionalCertificateIssueDate { get; set; }
    }
}
