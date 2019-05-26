using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "накладная", "накладная", "накладные", "")]
    public partial class Invoice
    {
        public Invoice()
        {
            Items = new List<InvoiceItem>();
        }

        [Browsable(false)]
        public int Id { get; protected set; }

        public Guid Guid { get; set; }

        [MaxLength(15)]
        [DataElement("ru", "номер документа", "номерДок", "")]
        public string Code { get; set; }

        public DateTime? DocDateTime { get; set; }

        public DateTime? ShippingDateTime { get; set; }

        public Organization Supplier { get; set; }

        public Organization Receiver { get; set; }

        public Organization Consignee { get; set; }

        public string PaymentConditions { get; set; }

        public string ProductGroup { get; set; }

        public decimal ManufacturerPrice { get; set; }

        public decimal ValueAddedTaxAmount { get; set; }

        public decimal TotalPrice { get => ManufacturerPrice + ValueAddedTaxAmount; }

        [MaxLength(1000)]
        public string Note { get; set; }

        public virtual List<InvoiceItem> Items { get; set; }

        public int ItemCount { get => Items.Count; }
    }
}
