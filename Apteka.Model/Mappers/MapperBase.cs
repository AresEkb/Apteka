using System;
using System.Collections.Generic;
using System.Linq;

using Apteka.Model.Entities;
using Apteka.Model.Entities.Base;
using Apteka.Model.Factories;

namespace Apteka.Model.Mappers
{
    public abstract class MapperBase
    {
        public MapperBase(IEntityFactory entityFactory)
        {
            EntityFactory = entityFactory;
        }

        protected IEntityFactory EntityFactory { get; private set; }

        private readonly IList<INamedEntity> localNamedEntities = new List<INamedEntity>();
        protected T FindOrCreateNamedEntity<T>(string name)
            where T : class, INamedEntity, new()
        {
            if (String.IsNullOrWhiteSpace(name)) { return null; }

            T entity = localNamedEntities.OfType<T>().FirstOrDefault(o => o.Name == name);
            if (entity != null) { return entity; }

            entity = EntityFactory.Query<T>().FirstOrDefault(o => o.Name == name);
            if (entity != null) { return entity; }

            entity = EntityFactory.Create<T>();
            entity.Name = name;
            localNamedEntities.Add(entity);
            return entity;
        }

        // TODO: Обобщить до FindOrCreateNamedEntity
        //private readonly IList<Country> localCountries = new List<Country>();
        //protected Country FindOrCreateCountry(string name)
        //{
        //    if (String.IsNullOrWhiteSpace(name))
        //    {
        //        return null;
        //    }
        //    Country country = localCountries.FirstOrDefault(o => o.Name == name);
        //    if (country != null) { return country; }

        //    country = EntityFactory.Query<Country>().FirstOrDefault(o => o.Name == name);
        //    if (country != null) { return country; }

        //    country = EntityFactory.Create<Country>();
        //    country.Name = name;
        //    localCountries.Add(country);
        //    return country;
        //}

        //private readonly IList<City> localCities = new List<City>();
        //protected City FindOrCreateCity(string name)
        //{
        //    if (String.IsNullOrWhiteSpace(name))
        //    {
        //        return null;
        //    }
        //    City city = localCities.FirstOrDefault(o => o.Name == name);
        //    if (city != null) { return city; }

        //    city = EntityFactory.Query<City>().FirstOrDefault(o => o.Name == name);
        //    if (city != null) { return city; }

        //    city = EntityFactory.Create<City>();
        //    city.Name = name;
        //    localCities.Add(city);
        //    return city;
        //}

        //private readonly IList<Organization> localOrganizations = new List<Organization>();
        //protected Organization FindOrCreateOrganization(string name)
        //{
        //    if (String.IsNullOrWhiteSpace(name))
        //    {
        //        return null;
        //    }
        //    Organization org = localOrganizations.FirstOrDefault(o => o.Name == name);
        //    if (org != null) { return org; }

        //    org = EntityFactory.Query<Organization>().FirstOrDefault(o => o.Name == name);
        //    if (org != null) { return org; }

        //    org = EntityFactory.Create<Organization>();
        //    org.Name = name;
        //    localOrganizations.Add(org);
        //    return org;
        //}

        protected BankAccount FindOrCreateBankAccount(Organization org,
            string bankCode, string bankName, string bankBranchName,
            string correspondentAccount, string checkingAccount)
        {
            if (String.IsNullOrWhiteSpace(checkingAccount))
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
    }
}
