using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "серия товаров", "серии товаров", "")]
    public class ProductSeries : EntityBase
    {
        [Key, Browsable(false)]
        public int Id { get; private set; }

        [DataElement("ru", "товарная позиция")]
        [Required]
        public virtual InvoiceItem InvoiceItem { get; set; }

        [DataElement("ru", "код")]
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
