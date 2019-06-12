using System;
using System.Text.RegularExpressions;

using Apteka.Model.Dtos;
using Apteka.Model.Entities;
using Apteka.Model.Extensions;
using Apteka.Model.Factories;
using Apteka.Model.Mappers.Base;

namespace Apteka.Model.Mappers
{
    public class StateMedicinePriceRegistryItemMapper : HashableObjectMapper<StateMedicinePriceRegistryItem, MedicineDosageForm>
    {
        public class Factory : IMapperFactory<StateMedicinePriceRegistryItem, MedicineDosageForm>
        {
            IMapper<StateMedicinePriceRegistryItem, MedicineDosageForm> IMapperFactory<StateMedicinePriceRegistryItem, MedicineDosageForm>.Create(IEntityFactory entityFactory, bool updateExisting, bool readOnlyFromCache)
            {
                return new StateMedicinePriceRegistryItemMapper(entityFactory, updateExisting, readOnlyFromCache);
            }
        }

        private StateMedicinePriceRegistryItemMapper(IEntityFactory entityFactory,
            bool updateExisting = false, bool readOnlyFromCache = false) : base(entityFactory, updateExisting, readOnlyFromCache)
        {
        }

        protected override void MapProperties(StateMedicinePriceRegistryItem dto, MedicineDosageForm entity)
        {
            /*
        public string DosageForms { get; set; }
        public string Organizations { get; set; }
        public string TotalCount { get; set; }
            */
            
            entity.Medicine = FindOrCreateMedicine(dto.TradeName, dto.Inn, "", dto.AtcCode, EntitySource);

            entity.RegistrationCertificateNumber = dto.RegistrationCertificateNumber;
            entity.Ean13 = dto.Ean13;

            entity.PriceLimit = dto.Price;
            entity.IsPrimaryPackagingPrice = !String.IsNullOrWhiteSpace(dto.PrimaryPackagingPrice);
            entity.PriceRegistrationDate = dto.PriceRegistrationDate;
            entity.PriceRegistrationDocNumber = dto.PriceRegistrationNumber;

            var ms = Regex.Matches(dto.DosageForms, "(.+?)(?<!РУ)( - |$)");
            string dosage = "", primary, intermediate, secondary;
            if (ms.Count == 1)
            {
                dosage = ms[0].Groups[1].Value;
            }
            else if (ms.Count == 2)
            {
                dosage = ms[0].Groups[1].Value;
                primary = ms[1].Groups[1].Value;
            }
            else if (ms.Count == 3)
            {
                dosage = ms[0].Groups[1].Value;
                primary = ms[1].Groups[1].Value;
                secondary = ms[2].Groups[1].Value;
            }
            else if (ms.Count == 3)
            {
                dosage = ms[0].Groups[1].Value;
                primary = ms[1].Groups[1].Value;
                intermediate = ms[2].Groups[1].Value;
                secondary = ms[3].Groups[1].Value;
            }
            if (dosage.TryMatchMeasure(out string dosage2, out decimal dosageFormMeasure, out string dosageFormUnit))
            {
                entity.DosageFormMeasure = dosageFormMeasure;
                entity.DosageFormMeasurementUnit = FindOrCreate<MeasurementUnit>(dosageFormUnit, EntitySource);
                if (dosage2.TryMatchMeasure(out string dosage3, out decimal dosageFormMeasure2, out string dosageFormUnit2))
                {
                    if (dosage3.TryMatchMeasure(out string dosage4, out decimal dosageMeasure, out string dosageUnit))
                    {
                        entity.DosageForm = FindOrCreate<DosageForm>(dosage4, EntitySource);
                        entity.AltDosageFormMeasure = dosageFormMeasure2;
                        entity.AltDosageFormMeasurementUnit = FindOrCreate<MeasurementUnit>(dosageFormUnit2, EntitySource);
                        entity.DosageMeasure = dosageMeasure;
                        entity.DosageMeasurementUnit = FindOrCreate<MeasurementUnit>(dosageUnit, EntitySource);
                    }
                    else
                    {
                        entity.DosageForm = FindOrCreate<DosageForm>(dosage3, EntitySource);
                        entity.DosageMeasure = dosageFormMeasure2;
                        entity.DosageMeasurementUnit = FindOrCreate<MeasurementUnit>(dosageFormUnit2, EntitySource);
                    }
                }
                else
                {
                    entity.DosageForm = FindOrCreate<DosageForm>(dosage2, EntitySource);
                }
            }
            else
            {
                entity.DosageForm = FindOrCreate<DosageForm>(dosage, EntitySource);
            }
            Console.WriteLine(ms.Count);

            //entity.RegistrationCertificateIssueDate = dto.RegistrationCertificateIssueDate.Value;
            //entity.RegistrationCertificateExpiryDate = dto.RegistrationCertificateExpiryDate;
            //entity.RegistrationCertificateCancellationDate = dto.RegistrationCertificateCancellationDate;
            //entity.CertificateRecipient = FindOrCreateOrganization(dto.CertificateRecipient, dto.CertificateRecipientCountry, EntitySource);

            //if (!IsEmptyName(dto.DosageForms))
            //{
            //    int comma = dto.DosageForms.LastIndexOf(',');
            //    string dosage = dto.DosageForms.Substring(0, comma).Trim();
            //    string packaging = dto.DosageForms.Substring(comma + 1);
            //    int minus = packaging.LastIndexOf(" - ");
            //    string packagingKind = packaging.Substring(0, minus).Trim();
            //    string packagingCount = packaging.Substring(minus + 3).Trim();

            //    if (dosage.EndsWith(" ~"))
            //    {
            //        entity.DosageForm = FindOrCreate<DosageForm>(dosage.Substring(0, dosage.Length - 2), EntitySource);
            //    }
            //    else
            //    {
            //        var m = Regex.Match(dosage, @"^(.*?[^+])\s+([0-9]+([.,][0-9]+)?)\s*([^0-9(),]{1,20})?$");
            //        if (m.Success)
            //        {
            //            entity.DosageForm = FindOrCreate<DosageForm>(m.Groups[1].Value, EntitySource);
            //            if (entity.DosageForm != null)
            //            {
            //                if (decimal.TryParse(m.Groups[2].Value.Replace(',', '.'), out decimal measure))
            //                {
            //                    entity.DosageMeasure = measure;
            //                    entity.DosageMeasurementUnit = FindOrCreate<MeasurementUnit>(m.Groups[4].Value, EntitySource);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            entity.DosageForm = FindOrCreate<DosageForm>(dosage, EntitySource);
            //        }
            //    }

            //    entity.PrimaryPackaging = FindOrCreate<PrimaryPackaging>(packagingKind, EntitySource);
            //    if (entity.PrimaryPackaging != null)
            //    {
            //        if (int.TryParse(packagingCount, out int count))
            //        {
            //            entity.PrimaryPackagingCount = count;
            //        }
            //    }
            //}

            //if (!IsEmptyName(dto.ManufactureStages))
            //{
            //    int comma = dto.ManufactureStages.LastIndexOf(',');
            //    string stages = dto.ManufactureStages.Substring(0, comma).Trim();
            //    string country = dto.ManufactureStages.Substring(comma + 1).Trim();
            //    var m = Regex.Match(stages, "^(.*?[^ ]),([^ ].*?)$");
            //    if (m.Success)
            //    {
            //        string role = m.Groups[1].Value;
            //        string orgAddress = m.Groups[2].Value;
            //        string org;
            //        string address;
            //        if (orgAddress.EndsWith(",") || orgAddress.EndsWith("~"))
            //        {
            //            // Address not specified
            //            org = orgAddress.TrimEnd(',', '~', ' ');
            //            address = "";
            //        }
            //        else
            //        {
            //            int comma2 = orgAddress.IndexOf(',');
            //            org = orgAddress.Substring(0, comma2);
            //            address = orgAddress.Substring(comma2 + 1);
            //        }
            //        var organization = FindOrCreateOrganization(org, country, EntitySource);
            //        if (organization != null)
            //        {
            //            if (!IsEmptyName(address))
            //            {
            //                if (organization.Address == null)
            //                {
            //                    organization.Address = EntityFactory.Create<Address>();
            //                }
            //                organization.Address.Country = organization.Country;
            //                organization.Address.Description = address;
            //            }
            //            var party = EntityFactory.Create<MedicineDosageFormOrganization>();
            //            party.Organization = organization;
            //            party.Role = FindOrCreate<OrganizationRole>(role.FirstCharToLower(), EntitySource);
            //            entity.Organizations.Add(party);
            //        }
            //    }
            //}

            //entity.NormativeDocument = dto.NormativeDocument;
            //if (Regex.IsMatch(dto.Package, "^[0-9]{13}") &&
            //    !Regex.IsMatch(dto.Package, "^0{13}"))
            //{
            //    entity.Ean13 = dto.Package.Substring(0, 13);
            //}
        }
    }
}
