using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public class ProductSeries
    {
        [Browsable(false)]
        public int Id { get; protected set; }

        [MaxLength(100)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string CertificateCode { get; set; }

        [MaxLength(100)]
        public string CertificationAuthority { get; set; }

        public DateTime? CertificateIssueDate { get; set; }

        public DateTime? CertificateExpireDate { get; set; }

        public DateTime? ShelfLifeDate { get; set; }
    }

    /*
                    <НомерСертиф>РОСС LV.ФМ09.Д92858</НомерСертиф>
                    <ОрганСертиф>ООО &quot;Институт фармацевтической
                        биотехнологии&quot;</ОрганСертиф>
                    <ДатаВыдачиСертиф>05.11.2015</ДатаВыдачиСертиф>
                    <СрокДействияСертиф>01.09.2019</СрокДействияСертиф>
                    <СрокГодностиТовара>01.09.2019</СрокГодностиТовара>
                    <РегНомер>_</РегНомер>
                    <РегНомерСертиф>92987-95</РегНомерСертиф>
                    <РегДатаСертиф>08.12.2015</РегДатаСертиф>
                    <РегОрганСертиф>ООО &quot;ОЦС&quot; г. Екатеринбург</РегОрганСертиф>
     */

    /* <ЗаголовокДокумента>
    <ТипДок>ПРХ</ТипДок><НомерДок>73109_Н10</НомерДок><ДатаДок>06.11.2018 13:35:59</ДатаДок>
    <ДатаОтгрузки>07.11.2010 9:05:50</ДатаОтгрузки><Поставщик>ЗАО &quot;Протек&quot;</Поставщик>
    <Получатель>7 Аптека Екатеринбург</Получатель><Грузополучатель>7 Аптека Екатеринбург</Грузополучатель>
    <УсловияОплаты>Отсрочка 14 дней</УсловияОплаты><ТоварнаяГруппа></ТоварнаяГруппа><Позиций>8</Позиций>
    <СуммаОпт>2661.95</СуммаОпт><СуммаНДС>372.25</СуммаНДС><СуммаОптВклНДС>3034.20</СуммаОптВклНДС>
    <Примечание></Примечание>
    
        <РеквизитыПоставщика><Адрес>ул. Ленина, 123</Адрес><ИНН>1234567890</ИНН>
    <Телефоны>12345678</Телефоны><РасчетныйСчет>12345678901234567890</РасчетныйСчет><Город>г. Екатеринбург</Город>
    <Банк>Уральский банк СБ РФ</Банк><ОтделениеБанка>Кировское отд. № 7003</ОтделениеБанка>
    <БИК>123456789</БИК><КорСчет>12434567890000000123</КорСчет><ЭлПочта>medicinfo@protek.ru</ЭлПочта>
    </РеквизитыПоставщика></ЗаголовокДокумента>
        */

    [DefaultClassOptions]
    public class Country
    {
        [Browsable(false)]
        public int Id { get; protected set; }

        [MaxLength(2)]
        public string Code { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }
    }

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
