using System;

using Apteka.Model.Dtos;
using Apteka.Model.Entities;
using Apteka.Model.Entities.Place;
using Apteka.Model.Factories;

namespace Apteka.Model.Mappers
{
    public class StateMedicineRegistryItemMapper : MapperBase
    {
        public StateMedicineRegistryItemMapper(IEntityFactory entityFactory) : base(entityFactory)
        {
        }

        //private readonly IList<MedicineDosageForm> localMedicineDosageForms = new List<MedicineDosageForm>();
        public MedicineDosageForm Map(StateMedicineRegistryItem dto, bool updateExisting = false, bool readOnlyFromCache = false)
        {
            var entitySource = readOnlyFromCache ? EntitySource.Cache : EntitySource.Both;
            var ean13 = dto.Package.Substring(0, 13);
            var entity = FindOrCreate<MedicineDosageForm>(e => e.Ean13 == ean13, updateExisting, entitySource);
            if (entity == null) { return null; }

            entity.Medicine = FindOrCreateMedicine(dto.TradeName, dto.Inn, dto.PharmacotherapeuticGroup, "", entitySource);
            //public DosageForm DosageForm { get; set; }
            //public decimal? DosageMeasure { get; set; }
            //public MeasurementUnit DosageMeasurementUnit { get; set; }
            //public decimal? DosageFormMeasure { get; set; }
            //public MeasurementUnit DosageFormMeasurementUnit { get; set; }
            //public PrimaryPackaging PrimaryPackaging { get; set; }
            //public int? PrimaryPackagingCount { get; set; }
            //public SecondaryPackaging SecondaryPackaging { get; set; }
            //public int? SecondaryPackagingCount { get; set; }
            //public SecondaryPackaging SecondaryPackaging2 { get; set; }
            //public int? PrimaryPackaging2Count { get; set; }
            //public int? TotalCount { get; set; }
            entity.RegistrationCertificateNumber = dto.RegistrationCertificateNumber;
            entity.RegistrationCertificateIssueDate = dto.RegistrationCertificateIssueDate.Value;
            entity.RegistrationCertificateExpiryDate = dto.RegistrationCertificateExpiryDate;
            entity.RegistrationCertificateCancellationDate = dto.RegistrationCertificateCancellationDate;
            entity.CertificateRecipient = FindOrCreate<Organization>(dto.CertificateRecipient, entitySource);
            entity.CertificateRecipientCountry = FindOrCreate<Country>(dto.CertificateRecipientCountry, entitySource);
            //public Organization Manufacturer { get; set; }
            entity.NormativeDocument = dto.NormativeDocument;
            entity.Ean13 = dto.Package.Substring(0, 13);
            return entity;
        }

        //private readonly IList<Medicine> localMedicines = new List<Medicine>();
        protected Medicine FindOrCreateMedicine(string tradeName, string inn,
            string pharmacotherapeuticGroup, string atcCode, EntitySource entitySource = EntitySource.Both)
        {
            if (String.IsNullOrWhiteSpace(tradeName)) { return null; }

            //var entity = localMedicines.FirstOrDefault(e => e.TradeName == tradeName);
            //if (entity != null) { return entity; }

            //entity = EntityFactory.Query<Medicine>().FirstOrDefault(o => o.TradeName == tradeName);
            //if (entity != null) { return entity; }

            var entity = EntityFactory.Find<Medicine>(o => o.TradeName.ToUpper().Equals(tradeName.ToUpper(), StringComparison.OrdinalIgnoreCase), entitySource);
            if (entity != null) { return entity; }

            entity = EntityFactory.Create<Medicine>();
            entity.TradeName = tradeName;
            if (!"~".Equals(inn))
            {
                entity.Inn = inn;
            }
            entity.PharmacotherapeuticGroup = FindOrCreate<PharmacotherapeuticGroup>(pharmacotherapeuticGroup, entitySource);
            entity.AtcCode = FindOrCreate<AtcGroup>(atcCode, entitySource);
            //localMedicines.Add(entity);
            return entity;
        }
    }
}
