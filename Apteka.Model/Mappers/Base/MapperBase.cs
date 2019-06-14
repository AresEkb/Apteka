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
        public MapperBase(IEntityFactory entityFactory, EntitySource entitySource)
        {
            EntityFactory = entityFactory;
            EntitySource = entitySource;
        }

        protected IEntityFactory EntityFactory { get; private set; }

        protected EntitySource EntitySource { get; private set; }

        // Empty names and ~, -, ...
        protected bool IsEmptyName(string name) =>
            String.IsNullOrWhiteSpace(name) || name.Length <= 1;

        protected T FindOrCreate<T>(Expression<Func<T, bool>> pred, bool returnExisting)
            where T : class, new()
        {
            var entity = EntityFactory.Find(pred, EntitySource);
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

        protected T FindOrCreate<T>(string name)
            where T : class, INamedEntity, new()
        {
            if (IsEmptyName(name)) { return null; }
            name = name.Trim();

            var entity = EntityFactory.Find<T>(e =>
                e.Name.ToUpper().Equals(name.ToUpper(), StringComparison.OrdinalIgnoreCase),
                EntitySource);
            if (entity != null) { return entity; }

            entity = EntityFactory.Create<T>();
            entity.Name = name;
            return entity;
        }

        protected Organization FindOrCreateOrganization(string organizationName, string countryName)
        {
            if (IsEmptyName(organizationName) ||
                IsEmptyName(countryName))
            {
                return null;
            }
            organizationName = organizationName.Trim();
            countryName = countryName.Trim();

            var entity = EntityFactory.Find<Organization>(e =>
                e.Name == organizationName &&
                e.Address != null &&
                e.Address.Country != null &&
                e.Address.Country.Name == countryName,
                EntitySource);
            if (entity != null) { return entity; }

            entity = EntityFactory.Create<Organization>();
            entity.Name = organizationName;
            entity.Address = EntityFactory.Create<Address>();
            entity.Address.Country = FindOrCreate<Country>(countryName);
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
            string pharmacotherapeuticGroup, string atcCode)
        {
            if (IsEmptyName(tradeName)) { return null; }
            tradeName = tradeName.Trim();

            var entity = EntityFactory.Find<Medicine>(o =>
                o.TradeName.ToUpper().Equals(tradeName.ToUpper(), StringComparison.OrdinalIgnoreCase),
                EntitySource);
            if (entity != null) { return entity; }

            entity = EntityFactory.Create<Medicine>();
            entity.TradeName = tradeName;
            if (!IsEmptyName(inn))
            {
                entity.Inn = inn.Trim();
            }
            entity.PharmacotherapeuticGroup = FindOrCreate<PharmacotherapeuticGroup>(pharmacotherapeuticGroup);
            entity.AtcCode = FindOrCreate<AtcGroup>(atcCode);
            return entity;
        }
    }
}
