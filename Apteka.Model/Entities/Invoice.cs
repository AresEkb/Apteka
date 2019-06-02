using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Apteka.Model.Annotations;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "накладная", "накладная", "накладные", "электронная товарная накладная")]
    [Category("Warehouse")]
    public class Invoice : IEntity
    {
        public Invoice()
        {
            Items = new List<InvoiceItem>();
            Guid = Guid.NewGuid();
        }

        [Key, Browsable(false)]
        public int Id { get; private set; }

        [UniqueIndex]
        public Guid Guid { get; set; }

        [DataElement("ru", "номер документа", "номерДок", "номер электронной накладной, обычно совпадает с номером счёт-фактуры")]
        [MaxLength(15)]
        public string Code { get; set; }

        [DataElement("ru", "дата документа", "датаДок", "дата электронной накладной")]
        public DateTime? DocDateTime { get; set; }

        [DataElement("ru", "дата отгрузки", "датаОтгрузки", "дата отгрузки товара")]
        public DateTime? ShipmentDateTime { get; set; }

        [DataElement("ru", "поставщик", "поставщик", "")]
        public virtual Organization Supplier { get; set; }

        [DataElement("ru", "банковский счет поставщика", "счет поставщика", "")]
        public virtual BankAccount SupplierBankAccount { get; set; }

        [DataElement("ru", "получатель", "получатель", "")]
        public virtual Organization Receiver { get; set; }

        [DataElement("ru", "грузополучатель", "грузополучатель", "")]
        public virtual Organization Consignee { get; set; }

        [DataElement("ru", "условия оплаты", "условия оплаты", "условия оплаты поставки")]
        public string PaymentConditions { get; set; }

        [DataElement("ru", "товарная группа", "товарная группа", "наименование товарной группы")]
        public string ProductGroup { get; set; }

        [DataElement("ru", "сумма оптовая", "cуммаОпт", "сумма предприятия поставщика без учета НДС")]
        public decimal SupplierPrice { get => Items.Sum(item => item.TotalSupplierPrice); }

        [DataElement("ru", "сумма НДС", "суммаНДС", "сумма НДС от цены предприятия поставщика")]
        public decimal ValueAddedTaxAmount { get => Items.Sum(item => item.ValueAddedTaxAmount); }

        [DataElement("ru", "сумма оптовая + НДС", "суммаОптВклНДС", "сумма предприятия поставщика с учётом НДС")]
        public decimal TotalPrice { get => SupplierPrice + ValueAddedTaxAmount; }

        [DataElement("ru", "примечание", "примечание", "примечание к документу")]
        [MaxLength(1000)]
        public string Note { get; set; }

        [DataElement("ru", "товарные позиции", "товарные позиции", "содержимое электронной накладной")]
        public virtual ICollection<InvoiceItem> Items { get; set; }

        [DataElement("ru", "количество позиций", "позиций", "количество позиций, входящих в данную поставку")]
        public int ItemCount { get => Items.Count; }
    }
}
