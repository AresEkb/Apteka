using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using DevExpress.Persistent.Base;

namespace Apteka.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class InvoiceItem
    {
        [Browsable(false)]
        public int Id { get; protected set; }

        [MaxLength(100)]
        public string ProductCode { get; set; }

        [MaxLength(100)]
        public string ProductName { get; set; }

        public virtual Organization Manufacturer { get; set; }

        public virtual Country ManufacturerCountry { get; set; }

        public int Quantity { get; set; }

        public decimal ManufacturerPrice { get; set; }

        public decimal StateRegistryPrice { get; set; }

        public decimal SupplierMarkupRate { get; set; }

        public decimal SupplierPrice { get => ManufacturerPrice * SupplierMarkupRate; }

        public decimal TotalSupplierPrice { get => SupplierPrice * Quantity; }

        public decimal ValueAddedTaxRate { get; set; }

        public decimal ValueAddedTaxAmount { get => TotalSupplierPrice * ValueAddedTaxRate; }

        public decimal TotalPrice { get => TotalSupplierPrice + ValueAddedTaxAmount; }

        [MaxLength(13)]
        public string Ean13 { get; set; }

        [MaxLength(30)]
        public string CustomsDeclarationNumber { get; set;}

        public virtual List<ProductSeries> Series { get; set; }
    }
}
