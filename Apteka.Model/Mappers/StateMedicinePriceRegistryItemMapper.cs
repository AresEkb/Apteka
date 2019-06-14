using System;
using System.Collections.Generic;
using System.Linq;
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
        // Нужно отслеживать новые роли и добавлять их сюда
        // Первая роль всегда должна быть ролью владельца РУ, владельцы хранятся
        // в отдельном поле в БД. Далее есть ссылка на эту роль: roles[0]
        private static readonly string[] roles = { "Вл.", "Пр.", "Вып.к.", "Уп.", "Перв.Уп.", "Втор.Уп." };

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

        // Исходный реестр очень замусоренный, поэтому такие сложные эвристики, подобранные опытным путем
        protected override void MapProperties(StateMedicinePriceRegistryItem dto, MedicineDosageForm entity)
        {
            entity.Medicine = FindOrCreateMedicine(dto.TradeName, dto.Inn, "", dto.AtcCode);

            entity.RegCertificateNumber = dto.RegistrationCertificateNumber;
            entity.Ean13 = dto.Ean13;

            entity.PriceLimit = dto.Price;
            entity.IsPrimaryPackagingPrice = !String.IsNullOrWhiteSpace(dto.PrimaryPackagingPrice);
            entity.PriceRegistrationDate = dto.PriceRegistrationDate;
            entity.PriceRegistrationDocNumber = dto.PriceRegistrationNumber;

            entity.TotalCount = dto.TotalCount;

            string fixedDosageForms = Regex.Replace(dto.DosageForms, @"\s{2,}", " ");
            fixedDosageForms = fixedDosageForms.Replace('–', '-').Replace(")-", ") -");
            // Часто встречается обрезанная информация об упаковке:
            // флаконы /в комплекте с растворителем - бактериостатическая
            // Добавляем в конец /, чтобы примечание корректно парсилось
            if (Regex.IsMatch(dto.DosageForms, "/в[^/]*?$"))
            {
                fixedDosageForms += "/";
            }

            // Дозировка и разные виды упаковок разделяются знаком минус с пробелами
            // Однако, они могут содержать в скобках пояснения, например "(в РУ - 10%)":
            // раствор для инфузий 100 мг/мл (в РУ - 10%), 1 шт., 100 мл - бутылки для крови и кровезаменителей - пачки картонные
            // Поэтому минусы внутри скобок игнорируем
            // Также добавляем исключения для таких ситуаций:
            // контейнеры пластиковые из ПВХ "Виафлексг" соединенные с Y-образной трубкой и пустым дренажным контейнером - система  "Твин Бэг"
            var ms = Regex.Matches(fixedDosageForms,
                "((.+?)([(].*?[)].*?|(?<= |[)])/.*?/(?= |-).*?)*)( - (?!система)|$)");
            string dosage = ms[0].Groups[1].Value;
            var packaging = ms.OfType<Match>().Skip(1)
                .Select(m => m.Groups[1].Value.Trim())
                // Иногда между видами упаковок нет минуса:
                // ампулы (5) упаковки ячейковые контурные (2)
                // Разделяем их, если встречаем количество в середине
                .SelectMany(SplitPackagingByCount)
                .ToArray();

            // Иногда сведения об упаковке обрезаны:
            // упаковки контурные пластиковые (поддоны) /в компле
            // В этом случае добавляем в конце /, чтобы примечание корректно распарсилось
            //if (packaging.Length > 0) {
            //    if (Regex.IsMatch(packaging[packaging.Length - 1], @"^.+?\s+/.+?[^/]$"))
            //    {
            //        packaging[packaging.Length - 1] += "/";
            //    }
            //}

            // Последняя величина в дозировке - это количество лекарственных форм (например, количество таблеток)
            // Вторая величина с конца (опциональная) - это количество в альтернативных единицах измерения
            // Третья величина с конца (либо вторая, если их всего две) - это дозировка
            if (dosage.TryMatchMeasure(out string dosage2, out decimal dosageFormMeasure, out string dosageFormUnit))
            {
                entity.DosageFormMeasure = dosageFormMeasure;
                entity.DosageFormMeasurementUnit = FindOrCreate<MeasurementUnit>(dosageFormUnit);
                if (dosage2.TryMatchMeasure(out string dosage3, out decimal dosageFormMeasure2, out string dosageFormUnit2))
                {
                    if (dosage3.TryMatchMeasure(out string dosage4, out decimal dosageMeasure, out string dosageUnit))
                    {
                        entity.DosageForm = FindOrCreate<DosageForm>(dosage4);
                        entity.AltDosageFormMeasure = dosageFormMeasure2;
                        entity.AltDosageFormMeasurementUnit = FindOrCreate<MeasurementUnit>(dosageFormUnit2);
                        entity.DosageMeasure = dosageMeasure;
                        entity.DosageMeasurementUnit = FindOrCreate<MeasurementUnit>(dosageUnit);
                    }
                    else
                    {
                        entity.DosageForm = FindOrCreate<DosageForm>(dosage3);
                        entity.DosageMeasure = dosageFormMeasure2;
                        entity.DosageMeasurementUnit = FindOrCreate<MeasurementUnit>(dosageFormUnit2);
                    }
                }
                else
                {
                    entity.DosageForm = FindOrCreate<DosageForm>(dosage2);
                }
            }
            else
            {
                entity.DosageForm = FindOrCreate<DosageForm>(dosage);
            }

            //string primary = "";
            //string intermediate = "";
            //string intermediate2 = "";
            //string secondary = "";
            //switch (packaging.Count())
            //{
            //    case 0:
            //        break;
            //    case 1:
            //        primary = packaging[0];
            //        break;
            //    case 2:
            //        primary = packaging[0];
            //        secondary = packaging[1];
            //        break;
            //    case 3:
            //        primary = packaging[0];
            //        intermediate = packaging[1];
            //        secondary = packaging[2];
            //        break;
            //    case 4:
            //        primary = packaging[0];
            //        intermediate = packaging[1];
            //        intermediate2 = packaging[2];
            //        secondary = packaging[3];
            //        break;
            //    default:
            //        throw new NotSupportedException();
            //}

            for (int i = 0; i < packaging.Length; i++)
            {
                MatchPackaging(packaging[i], out string kindMeasure, out int? count, out string note);
                if (IsEmptyName(kindMeasure))
                {
                    continue;
                }
                var pkg = EntityFactory.Create<MedicineDosageFormPackaging>();
                pkg.Order = (byte)(i + 1);
                pkg.Count = count;
                pkg.Note = note;
                if (kindMeasure.TryMatchMeasure(out string kind, out decimal measure, out string unit))
                {
                    pkg.Kind = FindOrCreate<PackagingKind>(kind);
                    pkg.Measure = measure;
                    pkg.MeasurementUnit = FindOrCreate<MeasurementUnit>(unit);
                }
                else
                {
                    pkg.Kind = FindOrCreate<PackagingKind>(kindMeasure);
                }
                if (i == 0)
                {
                    pkg.Kind.IsPrimary = true;
                }
                else if (i == packaging.Length - 1)
                {
                    pkg.Kind.IsSecondary = true;
                }
                else
                {
                    pkg.Kind.IsIntermediate = true;
                }
                entity.Packagings.Add(pkg);
            }

            // Для каждого вида упаковок берем из скобок количество упаковок
            // Из символов / / берем дополнительную информацию
            // TODO: !!!
            //if (!String.IsNullOrWhiteSpace(primary))
            //{
            //    MatchPackaging(primary, out string kind, out int? count, out string note);
            //    entity.PrimaryPackaging = FindOrCreate<PrimaryPackaging>(kind);
            //    entity.PrimaryPackagingCount = count;
            //    entity.PrimaryPackagingNote = note;
            //}
            //if (!String.IsNullOrWhiteSpace(intermediate))
            //{
            //    MatchPackaging(intermediate, out string kind, out int? count, out string note);
            //    entity.IntermediatePackaging = FindOrCreate<IntermediatePackaging>(kind);
            //    entity.IntermediatePackagingCount = count;
            //    entity.IntermediatePackagingNote = note;
            //}
            //if (!String.IsNullOrWhiteSpace(intermediate2))
            //{
            //    MatchPackaging(intermediate2, out string kind, out int? count, out string note);
            //    entity.IntermediatePackaging2 = FindOrCreate<IntermediatePackaging>(kind);
            //    entity.IntermediatePackaging2Count = count;
            //    entity.IntermediatePackaging2Note = note;
            //}
            //if (!String.IsNullOrWhiteSpace(secondary))
            //{
            //    MatchPackaging(secondary, out string kind, out int? count, out string note);
            //    entity.SecondaryPackaging = FindOrCreate<SecondaryPackaging>(kind);
            //    entity.SecondaryPackagingCount = count;
            //    entity.SecondaryPackagingNote = note;
            //}

            // Парсим организации
            var orgs = dto.Organizations
                .Split(';')
                // Иногда организации разделяются запятой и найти их можно только по роли
                .SelectMany(SplitOrganizationNameByRoles)
                .Select(o => o.Trim(' ', ','));
            foreach (var org in orgs)
            {
                int minus = org.LastIndexOf(" - ");
                // Бывает, что страна отделяется не минусом, а запятой
                // При этом минус есть где-то в середине названия организации
                // В этом случае в названии страны будет несколько точек, кавычек, минусов
                // Таким образом мы понимаем, что неправильно отделили страну и используем
                // в качестве разделителя запятую
                if (minus == -1 || org.Substring(minus + 1).Count(c => c == '.' || c == '"' || c == '-') > 2)
                {
                    minus = org.LastIndexOf(',');
                }
                // Изредка бывает, что страна указывается просто через пробел
                // TODO: В этом случае было бы более правильно искать страну по справочнику стран
                if (minus == -1)
                {
                    minus = org.LastIndexOf(' ');
                }
                // Если указаны только роли без организации, то пропускаем
                if (minus == -1)
                {
                    continue;
                }
                string nameRoles = org.Substring(0, minus);
                string country = org.Substring(minus + 1).Trim(' ', '\n', '-', '.', ',', '"');

                var roleNames = GetOrganizationRoles(nameRoles, out string orgName);
                var organization = FindOrCreateOrganization(orgName, country);
                if (roleNames.Count() == 0)
                {
                    entity.RegCertificateOwner = organization;
                }
                else
                {
                    foreach (string role in roleNames)
                    {
                        if (role == roles[0])
                        {
                            entity.RegCertificateOwner = organization;
                        }
                        else
                        {
                            var party = EntityFactory.Create<MedicineDosageFormOrganization>();
                            party.Organization = organization;
                            party.Role = FindOrCreate<OrganizationRole>(role);
                            entity.Organizations.Add(party);
                        }
                    }
                }
            }

            //Console.WriteLine(ms.Count);
        }

        private IEnumerable<string> SplitPackagingByCount(string str)
        {
            var result = new List<string>();
            var m = Regex.Match(str, @"^(.+?[(][0-9]+[)])\s+([^/+].+?)$");
            if (m.Success)
            {
                result.Add(m.Groups[1].Value);
                result.Add(m.Groups[2].Value);
            }
            else
            {
                result.Add(str);
            }
            return result;
        }

        private static void MatchPackaging(string str, out string packaging, out int? count, out string note)
        {
            // ^(.+?)[(]([0-9]+)[)]\s+/(.+?)/$
            // 1 2 3
            // ^(.+?)(\s+/(.+?)/\s+)?[(]([0-9]+)[)]$
            // 1 3? 4
            // ^(.+?)(\s+/(.+?)/)?$
            // 1 3?
            {
                var m = Regex.Match(str, @"^(.+?)[(]([0-9]+)[)]\s+/(.+?)/$");
                if (m.Success)
                {
                    packaging = m.Groups[1].Value.Trim();
                    count = int.Parse(m.Groups[2].Value);
                    note = m.Groups[3].Value.Trim();
                    return;
                }
            }
            {
                var m = Regex.Match(str, @"^(.+?)(\s+/(.+?)/\s+)?[(]([0-9]+)[)]$");
                if (m.Success)
                {
                    packaging = m.Groups[1].Value.Trim();
                    count = int.Parse(m.Groups[4].Value);
                    note = m.Groups[3].Value.Trim();
                    return;
                }
            }
            {
                var m = Regex.Match(str, @"^(.+?)(\s+/(.+?)/)?$");
                if (m.Success)
                {
                    packaging = m.Groups[1].Value.Trim();
                    count = null;
                    note = m.Groups[3].Value.Trim();
                    return;
                }
            }
            packaging = str;
            count = null;
            note = null;
            return;
        }

        private IEnumerable<string> SplitOrganizationNameByRoles(string name)
        {
            var result = new List<string>();
            int index;
            while ((index = name.IndexOfAny(roles)) >= 0)
            {
                result.Add(name.Substring(0, index));
                GetOrganizationRoles(name.Substring(index), out name);
            }
            if (result.Count == 0)
            {
                result.Add(name);
            }
            return result;
        }

        private IEnumerable<string> GetOrganizationRoles(string rolesAndName, out string name)
        {
            var roleNames = new List<string>();
            bool startsWithRole = true;
            while (startsWithRole)
            {
                startsWithRole = false;
                foreach (var role in roles)
                {
                    if (rolesAndName.StartsWith(role))
                    {
                        roleNames.Add(role);
                        rolesAndName = rolesAndName.Substring(role.Length).TrimStart(' ', ',', '-');
                        startsWithRole = true;
                    }
                }
            }
            name = rolesAndName;
            return roleNames;
        }
    }
}
