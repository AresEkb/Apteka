using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Apteka.Model.Entities
{
    public class InvoiceItem
    {
        public InvoiceItem()
        {
            Series = new List<ProductSeries>();
        }

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

        [DecimalPrecision(5, 4)]
        [DisplayFormat(DataFormatString = "{0:P0}", ApplyFormatInEditMode = true)]
        public decimal SupplierMarkupRate { get; set; }

        public decimal SupplierPrice { get => Math.Round(ManufacturerPrice * (1 + SupplierMarkupRate), 2); }

        public decimal TotalSupplierPrice { get => SupplierPrice * Quantity; }

        [DecimalPrecision(5, 4)]
        [DisplayFormat(DataFormatString = "{0:P0}", ApplyFormatInEditMode = true)]
        public decimal ValueAddedTaxRate { get; set; }

        public decimal ValueAddedTaxAmount { get => Math.Round(TotalSupplierPrice * ValueAddedTaxRate, 2); }

        public decimal TotalPrice { get => TotalSupplierPrice + ValueAddedTaxAmount; }

        [MinLength(13), MaxLength(13), NonUnicode]
        public string Ean13 { get; set; }

        [MaxLength(30)]
        public string CustomsDeclarationNumber { get; set;}

        public virtual List<ProductSeries> Series { get; set; }
    }
}
