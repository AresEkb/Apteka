using System;
using System.Linq;
using System.Linq.Expressions;

using Apteka.Model.Entities;
using Apteka.Model.Entities.Base;
using Apteka.Model.Entities.Place;
using Apteka.Model.Factories;

namespace Apteka.Model.Mappers.Base
{
    public abstract class MapperBase
    {
        public MapperBase(IEntityFactory entityFactory)
        {
            EntityFactory = entityFactory;
        }

        protected IEntityFactory EntityFactory { get; private set; }

        // Empty names and ~, -, ...
        protected bool IsEmptyName(string name) =>
            String.IsNullOrWhiteSpace(name) || name.Length <= 1;

        protected T FindOrCreate<T>(Expression<Func<T, bool>> pred, bool returnExisting, EntitySource entitySource = EntitySource.Both)
            where T : class, new()
        {
            var entity = EntityFactory.Find(pred, entitySource);
            if (entity != null)
            {
                if (!returnExisting) { return null; }
                EntityFactory.Attach(entity);
                return entity;
            }
            else
            {
                return EntityFactory.Create<T>();
            }
        }

        protected T FindOrCreate<T>(string name, EntitySource entitySource = EntitySource.Both)
            where T : class, INamedEntity, new()
        {
            name = name?.Trim();
            if (IsEmptyName(name)) { return null; }

            var entity = EntityFactory.Find<T>(e =>
                e.Name.ToUpper().Equals(name.ToUpper(), StringComparison.OrdinalIgnoreCase),
                entitySource);
            if (entity != null) { return entity; }

            entity = EntityFactory.Create<T>();
            entity.Name = name;
            return entity;
        }

        protected Organization FindOrCreateOrganization(string organizationName,
            string countryName, EntitySource entitySource = EntitySource.Both)
        {
            if (IsEmptyName(organizationName) ||
                IsEmptyName(countryName))
            {
                return null;
            }

            var entity = EntityFactory.Find<Organization>(e =>
                e.Name == organizationName &&
                e.Country != null &&
                e.Country.Name == countryName,
                entitySource);
            if (entity != null) { return entity; }

            entity = EntityFactory.Create<Organization>();
            entity.Name = organizationName;
            entity.Country = FindOrCreate<Country>(countryName, entitySource);
            return entity;
        }

        protected BankAccount FindOrCreateBankAccount(Organization org,
            string bankCode, string bankName, string bankBranchName,
            string correspondentAccount, string checkingAccount)
        {
            if (IsEmptyName(checkingAccount))
            {
                return null;
            }
            BankAccount account = org.BankAccounts.FirstOrDefault(acc =>
                acc.BankCode == bankCode &&
                acc.BankName == bankName &&
                acc.BankBranchName == bankBranchName &&
                acc.CorrespondentAccount == correspondentAccount &&
                acc.CheckingAccount == checkingAccount);
            if (account != null) { return account; }

            account = EntityFactory.Create<BankAccount>();
            account.Organization = org;
            account.BankCode = bankCode;
            account.BankName = bankName;
            account.BankBranchName = bankBranchName;
            account.CorrespondentAccount = correspondentAccount;
            account.CheckingAccount = checkingAccount;
            return account;
        }

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
