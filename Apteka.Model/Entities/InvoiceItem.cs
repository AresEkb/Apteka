using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "товарная позиция", "товарная позиция", "товарные позиции",
        "детальная информация о товаре, такая как наименование, изготовитель, " +
        "цены, суммы, серии и прочая информация")]
    public class InvoiceItem
    {
        public InvoiceItem()
        {
            Series = new List<ProductSeries>();
        }

        [Key, Browsable(false)]
        public int Id { get; private set; }

        [Required]
        public virtual Invoice Invoice { get; set; }

        [DataElement("ru", "код товара", "код товара", "код товара в справочнике товаров")]
        [MaxLength(100)]
        public string ProductCode { get; set; }

        [DataElement("ru", "товар", "товар", "наименование товарной позиции")]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [DataElement("ru", "изготовитель", "изготовитель", "предприятие изготовитель")]
        public virtual Organization Manufacturer { get; set; }

        [DataElement("ru", "страна изготовителя", "страна изготовителя", "страна предприятия изготовителя")]
        public virtual Country ManufacturerCountry { get; set; }

        [DataElement("ru", "количество", "кол-во", "количество натуральных единиц поставляемой позиции")]
        public int Quantity { get; set; }

        [DataElement("ru", "цена изготовителя", "ценаИзг", "отпускная цена предприятия изготовителя")]
        public decimal ManufacturerPrice { get; set; }

        [DataElement("ru", "цена по гос. реестру", "ценаГР", "цена по государственному реестру")]
        public decimal StateRegistryPrice { get; set; }

        [DataElement("ru", "оптовая наценка", "наценОпт", "наценка предприятия поставщика по отношению к цене завода изготовителя, выраженная в процентах")]
        [DecimalPrecision(5, 4)]
        [Range(0, 1)]
        [DisplayFormat(DataFormatString = "{0:p0}", ApplyFormatInEditMode = true)]
        public decimal SupplierMarkupRate { get; set; }

        [DataElement("ru", "оптовая цена", "ценаОпт", "цена предприятия поставщика без учета НДС")]
        public decimal SupplierPrice { get => Math.Round(ManufacturerPrice * (1 + SupplierMarkupRate), 2); }

        [DataElement("ru", "оптовая сумма", "суммаОпт", "сумма предприятия поставщика без учета НДС")]
        public decimal TotalSupplierPrice { get => SupplierPrice * Quantity; }

        [DataElement("ru", "ставка НДС", "ставкаНДС", "налоговая ставка НДС, выраженная в процентах")]
        [DecimalPrecision(5, 4)]
        [Range(0, 1)]
        [DisplayFormat(DataFormatString = "{0:p0}", ApplyFormatInEditMode = true)]
        public decimal ValueAddedTaxRate { get; set; }

        [DataElement("ru", "сумма НДС", "суммаНДС", "сумма НДС от цены предприятия поставщика ")]
        public decimal ValueAddedTaxAmount { get => Math.Round(TotalSupplierPrice * ValueAddedTaxRate, 2); }

        [DataElement("ru", "оптовая сумма + НДС", "суммаОптВклНДС", "сумма предприятия поставщика с учётом НДС")]
        public decimal TotalPrice { get => TotalSupplierPrice + ValueAddedTaxAmount; }

        [DataElement("ru", "EAN13", "EAN13", "внешний штрих-код формата EAN13")]
        [MinLength(13), MaxLength(13), NonUnicode]
        public string Ean13 { get; set; }

        [DataElement("ru", "таможенная декларация", "ГТД", "номер государственной таможенной декларации ")]
        [MaxLength(30)]
        public string CustomsDeclarationNumber { get; set;}

        [DataElement("ru", "серии", "серии", "информация о сериях и сертификатах для товарной позиции")]
        public virtual ICollection<ProductSeries> Series { get; set; }
    }
}
