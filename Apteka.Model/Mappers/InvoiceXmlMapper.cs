using System.Collections.Generic;
using System.Linq;

using Apteka.Model.Dto;
using Apteka.Model.Entities;
using Apteka.Model.Factories;

namespace Apteka.Model.Mapper
{
    public class InvoiceXmlMapper
    {
        private readonly IEntityFactory entityFactory;
        private readonly IQueryFactory queryFactory;

        public InvoiceXmlMapper(IEntityFactory entityFactory, IQueryFactory queryFactory)
        {
            this.entityFactory = entityFactory;
            this.queryFactory = queryFactory;
        }

        public Invoice Map(InvoiceXml dto)
        {
            var entity = entityFactory.Create<Invoice>();
            if (dto.ZagolovokDokumenta != null)
            {
                entity.Guid = dto.Identifikator;
                //dto.ZagolovokDokumenta.TipDok
                entity.Code = dto.ZagolovokDokumenta.NomerDok;
                entity.DocDateTime = dto.ZagolovokDokumenta.DataDok;
                entity.ShipmentDateTime = dto.ZagolovokDokumenta.DataOtgruzki;
                //dto.ZagolovokDokumenta.Postavshhik
                entity.Receiver = FindOrCreateOrganization(dto.ZagolovokDokumenta.Poluchatel);
                entity.Consignee = FindOrCreateOrganization(dto.ZagolovokDokumenta.Gruzopoluchatel);
                entity.PaymentConditions = dto.ZagolovokDokumenta.UslovijaOplaty;
                entity.ProductGroup = dto.ZagolovokDokumenta.TovarnajaGruppa;
                //dto.ZagolovokDokumenta.Pozicij
                //entity.SupplierPrice = dto.ZagolovokDokumenta.SummaOpt;
                //entity.ValueAddedTaxAmount = dto.ZagolovokDokumenta.SummaNDS;
                //invoice.TotalPrice = dto.ZagolovokDokumenta.SummaOptVklNDS;
                entity.Note = dto.ZagolovokDokumenta.Primechanie;
                //dto.ZagolovokDokumenta.RekvizityPostavshhika
            }
            if (dto.TovarnyePozicii != null)
            {
                entity.Items.AddRange(dto.TovarnyePozicii.Select(el => Map(el)));
            }
            return entity;
        }

        public InvoiceXml Map(Invoice entity)
        {
            var dto = new InvoiceXml();
            dto.ZagolovokDokumenta = new InvoiceXmlHeader();
            dto.Identifikator = entity.Guid;
            dto.ZagolovokDokumenta.TipDok = "ПРХ";
            dto.ZagolovokDokumenta.NomerDok = entity.Code;
            dto.ZagolovokDokumenta.DataDok = entity.DocDateTime;
            dto.ZagolovokDokumenta.DataOtgruzki = entity.ShipmentDateTime;
            //dto.ZagolovokDokumenta.Postavshhik
            dto.ZagolovokDokumenta.Poluchatel = entity.Receiver?.Name;
            dto.ZagolovokDokumenta.Gruzopoluchatel = entity.Consignee?.Name;
            dto.ZagolovokDokumenta.UslovijaOplaty = entity.PaymentConditions;
            dto.ZagolovokDokumenta.TovarnajaGruppa = entity.ProductGroup;
            dto.ZagolovokDokumenta.Pozicij = entity.ItemCount;
            dto.ZagolovokDokumenta.SummaOpt = entity.SupplierPrice;
            dto.ZagolovokDokumenta.SummaNDS = entity.ValueAddedTaxAmount;
            dto.ZagolovokDokumenta.SummaOptVklNDS = entity.TotalPrice;
            dto.ZagolovokDokumenta.Primechanie = entity.Note;
            //dto.ZagolovokDokumenta.RekvizityPostavshhika
            dto.TovarnyePozicii = new List<InvoiceXmlItem>(entity.Items.Select(el => Map(el)));
            return dto;
        }

        private InvoiceItem Map(InvoiceXmlItem dto)
        {
            var entity = entityFactory.Create<InvoiceItem>();
            entity.ProductCode = dto.KodTovara;
            entity.ProductName = dto.Tovar;
            //item.Manufacturer = dto.Izgotovitel;
            //item.ManufacturerCountry = dto.StranaIzgotovitelja;
            entity.Quantity = dto.Kolichestvo;
            entity.ManufacturerPrice = dto.CenaIzg;
            entity.StateRegistryPrice = dto.CenaGR;
            entity.SupplierMarkupRate = dto.NacenOpt / 100;
            //dto.CenaOpt
            //dto.SummaOpt
            entity.ValueAddedTaxRate = dto.StavkaNDS / 100;
            //dto.SummaNDS
            //dto.SummaOptVklNDS
            entity.Ean13 = dto.EAN13;
            entity.CustomsDeclarationNumber = dto.GTD;
            entity.Series.AddRange(dto.Serii.Select(el => Map(el)));
            return entity;
        }

        private readonly IList<Organization> localOrganizations = new List<Organization>();

        private Organization FindOrCreateOrganization(string name)
        {
            Organization org = localOrganizations.FirstOrDefault(o => o.Name == name);
            if (org != null) { return org; }

            org = queryFactory.Create<Organization>().FirstOrDefault(o => o.Name == name);
            if (org != null) { return org; }

            org = entityFactory.Create<Organization>();
            org.Name = name;
            localOrganizations.Add(org);
            return org;
        }

        private InvoiceXmlItem Map(InvoiceItem entity)
        {
            var dto = new InvoiceXmlItem();
            dto.KodTovara = entity.ProductCode;
            dto.Tovar = entity.ProductName;
            //dto.Izgotovitel = entity.ManufacturerName;
            //dto.StranaIzgotovitelja = entity.ManufacturerCountry;
            dto.Kolichestvo = entity.Quantity;
            dto.CenaIzg = entity.ManufacturerPrice;
            dto.CenaGR = entity.StateRegistryPrice;
            dto.NacenOpt = entity.SupplierMarkupRate * 100;
            dto.CenaOpt = entity.SupplierPrice;
            dto.SummaOpt = entity.TotalSupplierPrice;
            dto.StavkaNDS = entity.ValueAddedTaxRate * 100;
            dto.SummaNDS = entity.ValueAddedTaxAmount;
            dto.SummaOptVklNDS = entity.TotalPrice;
            dto.EAN13 = entity.Ean13;
            dto.GTD = entity.CustomsDeclarationNumber;
            dto.Serii = new List<InvoiceXmlSeries>(entity.Series.Select(el => Map(el)));
            return dto;
        }

        private ProductSeries Map(InvoiceXmlSeries dto)
        {
            var entity = entityFactory.Create<ProductSeries>();
            entity.Code = dto.SerijaTovara;
            entity.CertificateCode = dto.NomerSertif;
            //dto.OrganSertif;
            entity.CertificateIssueDate = dto.DataVydachiSertif;
            entity.CertificateExpireDate = dto.SrokDejstvijaSertif;
            entity.ShelfLifeDate = dto.SrokGodnostiTovara;
            //dto.RegNomer
            entity.RegionalCertificateCode = dto.RegNomerSertif;
            entity.RegionalCertificateIssueDate = dto.RegDataSertif;
            //dto.RegOrganSertif;
            return entity;
        }

        private InvoiceXmlSeries Map(ProductSeries entity)
        {
            var dto = new InvoiceXmlSeries();
            dto.SerijaTovara = entity.Code;
            dto.NomerSertif = entity.CertificateCode;
            //dto.OrganSertif;
            dto.DataVydachiSertif = entity.CertificateIssueDate;
            dto.SrokDejstvijaSertif = entity.CertificateExpireDate;
            dto.SrokGodnostiTovara = entity.ShelfLifeDate;
            dto.RegNomer = "_";
            dto.RegNomerSertif = entity.RegionalCertificateCode;
            dto.RegDataSertif = entity.RegionalCertificateIssueDate;
            //dto.RegOrganSertif;
            return dto;
        }
    }
}
