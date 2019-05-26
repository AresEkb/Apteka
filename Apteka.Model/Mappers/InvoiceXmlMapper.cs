using System.Collections.Generic;
using System.Linq;

using Apteka.Model.Dto;
using Apteka.Model.Entities;

namespace Apteka.Model.Mapper
{
    public class InvoiceXmlMapper
    {
        private readonly IEntityFactory entityFactory;

        public InvoiceXmlMapper(IEntityFactory entityFactory)
        {
            this.entityFactory = entityFactory;
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
                entity.ShippingDateTime = dto.ZagolovokDokumenta.DataOtgruzki;
                //dto.ZagolovokDokumenta.Postavshhik
                //dto.ZagolovokDokumenta.Poluchatel
                //dto.ZagolovokDokumenta.Gruzopoluchatel
                entity.PaymentConditions = dto.ZagolovokDokumenta.UslovijaOplaty;
                entity.ProductGroup = dto.ZagolovokDokumenta.TovarnajaGruppa;
                //dto.ZagolovokDokumenta.Pozicij
                entity.ManufacturerPrice = dto.ZagolovokDokumenta.SummaOpt;
                entity.ValueAddedTaxAmount = dto.ZagolovokDokumenta.SummaNDS;
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
            dto.ZagolovokDokumenta = new ZagolovokDokumenta();
            dto.Identifikator = entity.Guid;
            dto.ZagolovokDokumenta.TipDok = "ПРХ";
            dto.ZagolovokDokumenta.NomerDok = entity.Code;
            dto.ZagolovokDokumenta.DataDok = entity.DocDateTime;
            dto.ZagolovokDokumenta.DataOtgruzki = entity.ShippingDateTime;
            //dto.ZagolovokDokumenta.Postavshhik
            //dto.ZagolovokDokumenta.Poluchatel
            //dto.ZagolovokDokumenta.Gruzopoluchatel
            dto.ZagolovokDokumenta.UslovijaOplaty = entity.PaymentConditions;
            dto.ZagolovokDokumenta.TovarnajaGruppa = entity.ProductGroup;
            dto.ZagolovokDokumenta.Pozicij = entity.ItemCount;
            dto.ZagolovokDokumenta.SummaOpt = entity.ManufacturerPrice;
            dto.ZagolovokDokumenta.SummaNDS = entity.ValueAddedTaxAmount;
            dto.ZagolovokDokumenta.SummaOptVklNDS = entity.TotalPrice;
            dto.ZagolovokDokumenta.Primechanie = entity.Note;
            //dto.ZagolovokDokumenta.RekvizityPostavshhika
            dto.TovarnyePozicii = new List<TovarnajaPozicija>(entity.Items.Select(el => Map(el)));
            return dto;
        }

        private InvoiceItem Map(TovarnajaPozicija dto)
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

        private Organization FindOrCreateOrganization(string name)
        {
            IQueryable<Organization> organizations = null;
            Organization org = organizations.FirstOrDefault(o => o.Name == name);
            if (org == null)
            {
                org = entityFactory.Create<Organization>();
                org.Name = name;
            }
            return org;
        }

        private TovarnajaPozicija Map(InvoiceItem entity)
        {
            var dto = new TovarnajaPozicija();
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
            dto.Serii = new List<Serija>(entity.Series.Select(el => Map(el)));
            return dto;
        }

        private ProductSeries Map(Serija dto)
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

        private Serija Map(ProductSeries entity)
        {
            var dto = new Serija();
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
