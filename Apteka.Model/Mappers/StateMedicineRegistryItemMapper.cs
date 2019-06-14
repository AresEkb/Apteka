using System.Text.RegularExpressions;

using Apteka.Model.Dtos;
using Apteka.Model.Entities;
using Apteka.Model.Extensions;
using Apteka.Model.Factories;
using Apteka.Model.Mappers.Base;

namespace Apteka.Model.Mappers
{
    public class StateMedicineRegistryItemMapper : HashableObjectMapper<StateMedicineRegistryItem, MedicineDosageForm>
    {
        public class Factory : IMapperFactory<StateMedicineRegistryItem, MedicineDosageForm>
        {
            IMapper<StateMedicineRegistryItem, MedicineDosageForm> IMapperFactory<StateMedicineRegistryItem, MedicineDosageForm>.Create(IEntityFactory entityFactory, bool updateExisting, bool readOnlyFromCache)
            {
                return new StateMedicineRegistryItemMapper(entityFactory, updateExisting, readOnlyFromCache);
            }
        }

        private StateMedicineRegistryItemMapper(IEntityFactory entityFactory,
            bool updateExisting = false, bool readOnlyFromCache = false) : base(entityFactory, updateExisting, readOnlyFromCache)
        {
        }

        protected override void MapProperties(StateMedicineRegistryItem dto, MedicineDosageForm entity)
        {
            entity.Medicine = FindOrCreateMedicine(dto.TradeName, dto.Inn, dto.PharmacotherapeuticGroup, "");

            entity.RegCertificateNumber = dto.RegistrationCertificateNumber;
            entity.RegCertificateIssueDate = dto.RegistrationCertificateIssueDate.Value;
            entity.RegCertificateExpiryDate = dto.RegistrationCertificateExpiryDate;
            entity.RegCertificateCancellationDate = dto.RegistrationCertificateCancellationDate;
            entity.RegCertificateOwner = FindOrCreateOrganization(dto.CertificateRecipient, dto.CertificateRecipientCountry);

            if (!IsEmptyName(dto.DosageForms))
            {
                int comma = dto.DosageForms.LastIndexOf(',');
                string dosage = dto.DosageForms.Substring(0, comma).Trim();
                string packaging = dto.DosageForms.Substring(comma + 1);
                int minus = packaging.LastIndexOf(" - ");
                string packagingKind = packaging.Substring(0, minus).Trim();
                string packagingCount = packaging.Substring(minus + 3).Trim();

                if (dosage.EndsWith(" ~"))
                {
                    entity.DosageForm = FindOrCreate<DosageForm>(dosage.Substring(0, dosage.Length - 2));
                }
                else
                {
                    var m = Regex.Match(dosage, @"^(.*?[^+])\s+([0-9]+([.,][0-9]+)?)\s*([^0-9(),]{1,20})?$");
                    if (m.Success)
                    {
                        entity.DosageForm = FindOrCreate<DosageForm>(m.Groups[1].Value);
                        if (entity.DosageForm != null)
                        {
                            if (decimal.TryParse(m.Groups[2].Value.Replace(',', '.'), out decimal measure))
                            {
                                entity.DosageMeasure = measure;
                                entity.DosageMeasurementUnit = FindOrCreate<MeasurementUnit>(m.Groups[4].Value);
                            }
                        }
                    }
                    else
                    {
                        entity.DosageForm = FindOrCreate<DosageForm>(dosage);
                    }
                }

                // TODO: !!!
                //entity.PrimaryPackaging = FindOrCreate<PrimaryPackaging>(packagingKind);
                //if (entity.PrimaryPackaging != null)
                //{
                //    if (int.TryParse(packagingCount, out int count))
                //    {
                //        entity.PrimaryPackagingCount = count;
                //    }
                //}
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
                    var organization = FindOrCreateOrganization(org, country);
                    if (organization != null)
                    {
                        if (!IsEmptyName(address))
                        {
                            //if (organization.Address == null)
                            //{
                            //    organization.Address = EntityFactory.Create<Address>();
                            //}
                            //organization.Address.Country = organization.Country;
                            organization.Address.Description = address;
                        }
                        var party = EntityFactory.Create<MedicineDosageFormOrganization>();
                        party.Organization = organization;
                        party.Role = FindOrCreate<OrganizationRole>(role.FirstCharToLower());
                        entity.Organizations.Add(party);
                    }
                }
            }

            entity.NormativeDocument = dto.NormativeDocument;
            if (Regex.IsMatch(dto.Package, "^[0-9]{13}") &&
                !Regex.IsMatch(dto.Package, "^0{13}"))
            {
                entity.Ean13 = dto.Package.Substring(0, 13);
            }
        }
    }
}
