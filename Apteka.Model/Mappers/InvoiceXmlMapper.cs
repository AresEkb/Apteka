using System;
using System.Collections.Generic;
using System.Linq;

using Apteka.Model.Dtos;
using Apteka.Model.Entities;
using Apteka.Model.Entities.Place;
using Apteka.Model.Extensions;
using Apteka.Model.Factories;
using Apteka.Model.Mappers.Base;

namespace Apteka.Model.Mappers
{
    public class InvoiceXmlMapper : MapperBase
    {
        public InvoiceXmlMapper(IEntityFactory entityFactory)
            : base(entityFactory, EntitySource.DataBase)
        {
        }

        public Invoice Map(InvoiceXml dto)
        {
            var entity = EntityFactory.Create<Invoice>();
            if (dto.ZagolovokDokumenta != null)
            {
                var header = dto.ZagolovokDokumenta;
                entity.Guid = dto.Identifikator;
                //header.TipDok
                entity.Code = header.NomerDok;
                entity.DocDateTime = header.DataDok;
                entity.ShipmentDateTime = header.DataOtgruzki;
                entity.Supplier = FindOrCreate<Organization>(header.Postavshhik);
                entity.Receiver = FindOrCreate<Organization>(header.Poluchatel);
                entity.Consignee = FindOrCreate<Organization>(header.Gruzopoluchatel);
                entity.PaymentConditions = header.UslovijaOplaty;
                entity.ProductGroup = header.TovarnajaGruppa;
                //header.Pozicij
                //entity.SupplierPrice = header.SummaOpt;
                //entity.ValueAddedTaxAmount = header.SummaNDS;
                //invoice.TotalPrice = header.SummaOptVklNDS;
                entity.Note = header.Primechanie;
                if (entity.Supplier != null && header.RekvizityPostavshhika != null)
                {
                    var postavshik = header.RekvizityPostavshhika;
                    var city = FindOrCreate<City>(postavshik.Gorod);
                    if (city != null)
                    {
                        entity.Supplier.Address = EntityFactory.Create<Address>();
                        entity.Supplier.Address.City = city;
                    }
                    if (!String.IsNullOrWhiteSpace(postavshik.Adres))
                    {
                        if (entity.Supplier.Address == null)
                        {
                            entity.Supplier.Address = EntityFactory.Create<Address>();
                        }
                        entity.Supplier.Address.Description = postavshik.Adres;
                    }
                    entity.Supplier.TaxpayerCode = postavshik.INN;
                    entity.Supplier.PhoneNumber = postavshik.Telefony;
                    entity.Supplier.Email = postavshik.JelPochta;
                    entity.SupplierBankAccount = FindOrCreateBankAccount(entity.Supplier,
                        postavshik.BIK, postavshik.Bank, postavshik.OtdelenieBanka,
                        postavshik.KorSchet, postavshik.RaschetnyjSchet);
                }
            }
            if (dto.TovarnyePozicii != null)
            {
                entity.Items.AddRange(dto.TovarnyePozicii.Select(Map));
            }
            return entity;
        }

        public InvoiceXml Map(Invoice entity)
        {
            var dto = new InvoiceXml
            {
                Identifikator = entity.Guid,
                ZagolovokDokumenta = new InvoiceXmlHeader
                {
                    TipDok = "ПРХ",
                    NomerDok = entity.Code,
                    DataDok = entity.DocDateTime,
                    DataOtgruzki = entity.ShipmentDateTime,
                    Postavshhik = entity.Supplier?.Name,
                    Poluchatel = entity.Receiver?.Name,
                    Gruzopoluchatel = entity.Consignee?.Name,
                    UslovijaOplaty = entity.PaymentConditions,
                    TovarnajaGruppa = entity.ProductGroup,
                    Pozicij = entity.ItemCount,
                    SummaOpt = entity.SupplierPrice,
                    SummaNDS = entity.ValueAddedTaxAmount,
                    SummaOptVklNDS = entity.TotalPrice,
                    Primechanie = entity.Note
                }
            };
            if (entity.Supplier != null)
            {
                dto.ZagolovokDokumenta.RekvizityPostavshhika = new InvoiceXmlSupplier()
                {
                    Adres = entity.Supplier.Address?.Description,
                    INN = entity.Supplier.TaxpayerCode,
                    Telefony = entity.Supplier.PhoneNumber,
                    RaschetnyjSchet = entity.SupplierBankAccount?.CheckingAccount,
                    Gorod = entity.Supplier.Address?.City?.Name,
                    Bank = entity.SupplierBankAccount?.BankName,
                    OtdelenieBanka = entity.SupplierBankAccount?.BankBranchName,
                    BIK = entity.SupplierBankAccount?.BankCode,
                    KorSchet = entity.SupplierBankAccount?.CorrespondentAccount,
                    JelPochta = entity.Supplier.Email,
                };
            }
            dto.TovarnyePozicii = new List<InvoiceXmlItem>(entity.Items.Select(Map));
            return dto;
        }

        private InvoiceItem Map(InvoiceXmlItem dto)
        {
            var entity = EntityFactory.Create<InvoiceItem>();
            entity.ProductCode = dto.KodTovara;
            entity.ProductName = dto.Tovar;
            entity.Manufacturer = FindOrCreateOrganization(dto.Izgotovitel, dto.StranaIzgotovitelja);
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
            entity.Series.AddRange(dto.Serii.Select(Map));
            return entity;
        }

        private InvoiceXmlItem Map(InvoiceItem entity)
        {
            return new InvoiceXmlItem
            {
                KodTovara = entity.ProductCode,
                Tovar = entity.ProductName,
                Izgotovitel = entity.Manufacturer?.Name,
                StranaIzgotovitelja = entity.Manufacturer?.Address?.Country?.Name,
                Kolichestvo = entity.Quantity,
                CenaIzg = entity.ManufacturerPrice,
                CenaGR = entity.StateRegistryPrice,
                NacenOpt = entity.SupplierMarkupRate * 100,
                CenaOpt = entity.SupplierPrice,
                SummaOpt = entity.TotalSupplierPrice,
                StavkaNDS = entity.ValueAddedTaxRate * 100,
                SummaNDS = entity.ValueAddedTaxAmount,
                SummaOptVklNDS = entity.TotalPrice,
                EAN13 = entity.Ean13,
                GTD = entity.CustomsDeclarationNumber,
                Serii = new List<InvoiceXmlSeries>(entity.Series.Select(Map))
            };
        }

        private ProductSeries Map(InvoiceXmlSeries dto)
        {
            var entity = EntityFactory.Create<ProductSeries>();
            entity.Code = dto.SerijaTovara;
            entity.CertificateCode = dto.NomerSertif;
            entity.CertificateAuthority = dto.OrganSertif;
            entity.CertificateIssueDate = dto.DataVydachiSertif;
            entity.CertificateExpireDate = dto.SrokDejstvijaSertif;
            entity.ShelfLifeDate = dto.SrokGodnostiTovara;
            //dto.RegNomer
            entity.RegionalCertificateCode = dto.RegNomerSertif;
            entity.RegionalCertificateIssueDate = dto.RegDataSertif;
            entity.RegionalCertificateAuthority = dto.RegOrganSertif;
            return entity;
        }

        private InvoiceXmlSeries Map(ProductSeries entity)
        {
            return new InvoiceXmlSeries
            {
                SerijaTovara = entity.Code,
                NomerSertif = entity.CertificateCode,
                OrganSertif = entity.CertificateAuthority,
                DataVydachiSertif = entity.CertificateIssueDate,
                SrokDejstvijaSertif = entity.CertificateExpireDate,
                SrokGodnostiTovara = entity.ShelfLifeDate,
                RegNomer = "_",
                RegNomerSertif = entity.RegionalCertificateCode,
                RegDataSertif = entity.RegionalCertificateIssueDate,
                RegOrganSertif = entity.RegionalCertificateAuthority
            };
        }
    }
}
