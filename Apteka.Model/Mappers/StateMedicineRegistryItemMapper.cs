using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Apteka.Model.Dtos;
using Apteka.Model.Entities;
using Apteka.Model.Entities.Place;
using Apteka.Model.Factories;

namespace Apteka.Model.Mappers
{
    public class StateMedicineRegistryItemMapper : MapperBase
    {
        private readonly bool updateExisting;
        private readonly bool readOnlyFromCache;
        private readonly EntitySource entitySource;
        private readonly HashSet<string> numbers;

        public StateMedicineRegistryItemMapper(IEntityFactory entityFactory,
            bool updateExisting = false, bool readOnlyFromCache = false) : base(entityFactory)
        {
            this.updateExisting = updateExisting;
            this.readOnlyFromCache = readOnlyFromCache;

            entitySource = readOnlyFromCache ? EntitySource.Cache : EntitySource.Both;

            if (!updateExisting)
            {
                numbers = new HashSet<string>(
                    EntityFactory.Query<MedicineDosageForm>()
                        .Select(df => df.RegistrationCertificateNumber));
            }
        }

        public MedicineDosageForm Map(StateMedicineRegistryItem dto)
        {
            MedicineDosageForm entity = null;
            // If we will update records then we have to load all columns
            if (updateExisting)
            {
                entity = FindOrCreate<MedicineDosageForm>(e =>
                    e.RegistrationCertificateNumber == dto.RegistrationCertificateNumber,
                    updateExisting, entitySource);
            }
            // If we will not update records then it's much more effective to load keys only
            else if (!numbers.Contains(dto.RegistrationCertificateNumber))
            {
                entity = EntityFactory.Create<MedicineDosageForm>();
                entity.RegistrationCertificateNumber = dto.RegistrationCertificateNumber;
            }

            if (entity == null) { return null; }

            entity.Medicine = FindOrCreateMedicine(dto.TradeName, dto.Inn, dto.PharmacotherapeuticGroup, "", entitySource);

            if (!IsEmptyName(dto.DosageForms)) {
                int comma = dto.DosageForms.LastIndexOf(',');
                string dosage = dto.DosageForms.Substring(0, comma).Trim();
                string packaging = dto.DosageForms.Substring(comma + 1);
                int minus = packaging.LastIndexOf(" - ");
                string packagingKind = packaging.Substring(0, minus).Trim();
                string packagingCount = packaging.Substring(minus + 3).Trim();

                if (dosage.EndsWith(" ~"))
                {
                    entity.DosageForm = FindOrCreate<DosageForm>(dosage.Substring(0, dosage.Length - 2), entitySource);
                }
                else
                {
                    var m = Regex.Match(dosage, @"^(.*?[^+])\s+([0-9]+([.,][0-9]+)?)\s*([^0-9(),]{1,20})?$");
                    if (m.Success)
                    {
                        entity.DosageForm = FindOrCreate<DosageForm>(m.Groups[1].Value, entitySource);
                        if (entity.DosageForm != null)
                        {
                            if (decimal.TryParse(m.Groups[2].Value.Replace(',', '.'), out decimal measure))
                            {
                                entity.DosageMeasure = measure;
                                entity.DosageMeasurementUnit = FindOrCreate<MeasurementUnit>(m.Groups[4].Value, entitySource);
                            }
                        }
                    }
                    else
                    {
                        entity.DosageForm = FindOrCreate<DosageForm>(dosage, entitySource);
                    }
                }

                entity.PrimaryPackaging = FindOrCreate<PrimaryPackaging>(packagingKind, entitySource);
                if (entity.PrimaryPackaging != null)
                {
                    if (int.TryParse(packagingCount, out int count))
                    {
                        entity.PrimaryPackagingCount = count;
                    }
                }
            }

            if (!IsEmptyName(dto.ManufactureStages))
            {
                int comma = dto.ManufactureStages.LastIndexOf(',');
                string stages = dto.ManufactureStages.Substring(0, comma).Trim();
                string country = dto.ManufactureStages.Substring(comma + 1).Trim();
                var m = Regex.Match(stages, "^(.*?[^ ]),([^ ].*?)$");
                if (m.Success)
                {
                    string role = m.Groups[1].Value;
                    string orgAddress = m.Groups[2].Value;
                    string org;
                    string address;
                    if (orgAddress.EndsWith(",") || orgAddress.EndsWith("~"))
                    {
                        // Address not specified
                        org = orgAddress.TrimEnd(',', '~', ' ');
                        address = "";
                    }
                    else
                    {
                        int comma2 = orgAddress.IndexOf(',');
                        org = orgAddress.Substring(0, comma2);
                        address = orgAddress.Substring(comma2 + 1);
                    }
                    var organization = FindOrCreateOrganization(org, country, entitySource);
                    if (organization != null)
                    {
                        if (!IsEmptyName(address))
                        {
                            if (organization.Address == null)
                            {
                                organization.Address = EntityFactory.Create<Address>();
                            }
                            organization.Address.Country = organization.Country;
                            organization.Address.Description = address;
                        }
                        var party = EntityFactory.Create<MedicineDosageFormOrganization>();
                        party.Organization = organization;
                        party.Role = FindOrCreate<OrganizationRole>(role, entitySource);
                        entity.Organizations.Add(party);
                    }
                }
            }

            entity.RegistrationCertificateIssueDate = dto.RegistrationCertificateIssueDate.Value;
            entity.RegistrationCertificateExpiryDate = dto.RegistrationCertificateExpiryDate;
            entity.RegistrationCertificateCancellationDate = dto.RegistrationCertificateCancellationDate;
            entity.CertificateRecipient = FindOrCreateOrganization(dto.CertificateRecipient, dto.CertificateRecipientCountry, entitySource);
            entity.NormativeDocument = dto.NormativeDocument;
            if (Regex.IsMatch(dto.Package, "^[0-9]{13}") &&
                !Regex.IsMatch(dto.Package, "^0{13}"))
            {
                entity.Ean13 = dto.Package.Substring(0, 13);
            }
            return entity;
        }

        //private readonly IList<Medicine> localMedicines = new List<Medicine>();
        protected Medicine FindOrCreateMedicine(string tradeName, string inn,
            string pharmacotherapeuticGroup, string atcCode, EntitySource entitySource = EntitySource.Both)
        {
            tradeName = tradeName?.Trim();
            if (IsEmptyName(tradeName)) { return null; }

            var entity = EntityFactory.Find<Medicine>(o =>
                o.TradeName.ToUpper().Equals(tradeName.ToUpper(), StringComparison.OrdinalIgnoreCase),
                entitySource);
            if (entity != null) { return entity; }

            entity = EntityFactory.Create<Medicine>();
            entity.TradeName = tradeName;
            if (!IsEmptyName(inn))
            {
                entity.Inn = inn;
            }
            entity.PharmacotherapeuticGroup = FindOrCreate<PharmacotherapeuticGroup>(pharmacotherapeuticGroup, entitySource);
            entity.AtcCode = FindOrCreate<AtcGroup>(atcCode, entitySource);
            return entity;
        }
    }
}
