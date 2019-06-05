using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Apteka.Model.Annotations;
using Apteka.Model.Entities.Base;

namespace Apteka.Model.Entities
{
    [DataElement("ru", "накладная", "накладные", "электронная товарная накладная")]
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

        [DataElement("ru", "номер документа", "номер электронной накладной, обычно совпадает с номером счёт-фактуры", BriefName = "номерДок")]
        [MaxLength(15)]
        public string Code { get; set; }

        [DataElement("ru", "дата документа", "дата электронной накладной", BriefName = "датаДок")]
        public DateTime? DocDateTime { get; set; }

        [DataElement("ru", "дата отгрузки", "дата отгрузки товара", BriefName = "датаОтгрузки")]
        public DateTime? ShipmentDateTime { get; set; }

        [DataElement("ru", "поставщик")]
        public virtual Organization Supplier { get; set; }

        [DataElement("ru", "банковский счет поставщика", BriefName = "счет поставщика")]
        public virtual BankAccount SupplierBankAccount { get; set; }

        [DataElement("ru", "получатель")]
        public virtual Organization Receiver { get; set; }

        [DataElement("ru", "грузополучатель")]
        public virtual Organization Consignee { get; set; }

        [DataElement("ru", "условия оплаты", "условия оплаты поставки")]
        public string PaymentConditions { get; set; }

        [DataElement("ru", "товарная группа", "наименование товарной группы")]
        public string ProductGroup { get; set; }

        [DataElement("ru", "сумма оптовая", "сумма предприятия поставщика без учета НДС", BriefName = "cуммаОпт")]
        public decimal SupplierPrice => Items.Sum(item => item.TotalSupplierPrice);

        [DataElement("ru", "сумма НДС", "сумма НДС от цены предприятия поставщика", BriefName = "суммаНДС")]
        public decimal ValueAddedTaxAmount => Items.Sum(item => item.ValueAddedTaxAmount);

        [DataElement("ru", "сумма оптовая + НДС", "сумма предприятия поставщика с учётом НДС", BriefName = "суммаОптВклНДС")]
        public decimal TotalPrice => SupplierPrice + ValueAddedTaxAmount;

        [DataElement("ru", "примечание", "примечание к документу")]
        [MaxLength(1000)]
        public string Note { get; set; }

        [DataElement("ru", "товарные позиции", "содержимое электронной накладной")]
        public virtual ICollection<InvoiceItem> Items { get; set; }

        [DataElement("ru", "количество позиций", "количество позиций, входящих в данную поставку", BriefName = "позиций")]
        public int ItemCount => Items.Count;
    }
}
