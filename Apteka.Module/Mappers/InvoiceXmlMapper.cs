using Apteka.Model.Dto;
using Apteka.Module.BusinessObjects;

namespace Apteka.Model.Mapper
{
    public class InvoiceXmlMapper
    {
        private readonly IObjectFactory objectFactory;

        public InvoiceXmlMapper(IObjectFactory objectFactory)
        {
            this.objectFactory = objectFactory;
        }

        public Invoice MapToModel(InvoiceXml dto)
        {
            var invoice = objectFactory.Create<Invoice>();
            if (dto.ZagolovokDokumenta != null)
            {
                //dto.Identifikator
                //dto.ZagolovokDokumenta.TipDok
                invoice.Code = dto.ZagolovokDokumenta.NomerDok;
                invoice.DocDateTime = dto.ZagolovokDokumenta.DataDok;
                invoice.ShippingDateTime = dto.ZagolovokDokumenta.DataOtgruzki;
                //dto.ZagolovokDokumenta.Postavshhik
                //dto.ZagolovokDokumenta.Poluchatel
                //dto.ZagolovokDokumenta.Gruzopoluchatel
                invoice.PaymentConditions = dto.ZagolovokDokumenta.UslovijaOplaty;
                invoice.ProductGroup = dto.ZagolovokDokumenta.TovarnajaGruppa;
                //dto.ZagolovokDokumenta.Pozicij
                invoice.ManufacturerPrice = dto.ZagolovokDokumenta.SummaOpt;
                invoice.ValueAddedTaxAmount = dto.ZagolovokDokumenta.SummaNDS;
                //invoice.TotalPrice = dto.ZagolovokDokumenta.SummaOptVklNDS;
                invoice.Note = dto.ZagolovokDokumenta.Primechanie;
                //dto.ZagolovokDokumenta.RekvizityPostavshhika
            }
            if (dto.TovarnyePozicii != null)
            {
                foreach (var src in dto.TovarnyePozicii)
                {
                    var item = objectFactory.Create<InvoiceItem>();
                    item.ProductCode = src.KodTovara;
                    item.ProductName = src.Tovar;
                    //item.Manufacturer = src.Izgotovitel;
                    //item.ManufacturerCountry = src.StranaIzgotovitelja;
                    item.Quantity = src.Kolichestvo;
                    item.ManufacturerPrice = src.CenaIzg;
                    item.StateRegistryPrice = src.CenaGR;
                    item.SupplierMarkupRate = src.NacenOpt;
                    //src.CenaOpt
                    //src.SummaOpt
                    //src.StavkaNDS
                    //src.SummaNDS
                    //src.SummaOptVklNDS
                    item.Ean13 = src.EAN13;
                    item.CustomsDeclarationNumber = src.GTD;
                    foreach (var srcSeries in src.Serii)
                    {
                        var series = objectFactory.Create<ProductSeries>();
                        series.Code = srcSeries.SerijaTovara;
                        series.CertificateCode = srcSeries.NomerSertif;
                        //srcSeries.OrganSertif;
                        series.CertificateIssueDate = srcSeries.DataVydachiSertif;
                        series.CertificateExpireDate = srcSeries.SrokDejstvijaSertif;
                        series.ShelfLifeDate = srcSeries.SrokGodnostiTovara;
                        //srcSeries.RegNomer;
                        series.RegionalCertificateCode = srcSeries.RegNomerSertif;
                        series.RegionalCertificateIssueDate = srcSeries.RegDataSertif;
                        //srcSeries.RegOrganSertif;
                        item.Series.Add(series);
                    }
                    invoice.Items.Add(item);
                }
            }
            return invoice;
        }
    }
}
