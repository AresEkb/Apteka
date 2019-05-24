using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using DevExpress.Persistent.Base;

namespace Apteka.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Invoice
    {
        [Browsable(false)]
        public int Id { get; protected set; }

        [MaxLength(15)]
        public string Code { get; set; }

        public DateTime? DocDateTime { get; set; }

        public DateTime? ShippingDateTime { get; set; }

        public Organization Supplier { get; set; }

        public Organization Receiver { get; set; }

        public Organization Consignee { get; set; }

        public string PaymentConditions { get; set; }

        public string ProductGroup { get; set; }

        public decimal ManufacturerPrice { get; set; }

        public decimal ValueAddedTax { get; set; }

        public decimal TotalPrice { get => ManufacturerPrice + ValueAddedTax; }

        [MaxLength(1000)]
        public string Note { get; set; }

        public virtual List<InvoiceItem> Items { get; set; }
    }
}
