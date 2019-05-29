using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using Apteka.Model.Annotations;
using Apteka.Model.Entities;
using Apteka.Module.ModelExtensions;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.Validation;

namespace Apteka.Module
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppModuleBasetopic.aspx.
    public sealed partial class AptekaModule : ModuleBase
    {
        static AptekaModule()
        {
            DevExpress.Data.Linq.CriteriaToEFExpressionConverter.SqlFunctionsType = typeof(System.Data.Entity.SqlServer.SqlFunctions);
            DevExpress.Data.Linq.CriteriaToEFExpressionConverter.EntityFunctionsType = typeof(System.Data.Entity.DbFunctions);
            DevExpress.ExpressApp.SystemModule.ResetViewSettingsController.DefaultAllowRecreateView = false;
            // Uncomment this code to delete and recreate the database each time the data model has changed.
            // Do not use this code in a production environment to avoid data loss.
            // #if DEBUG
            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AptekaDbContext>());
            // #endif 
        }

        public AptekaModule()
        {
            InitializeComponent();
            DevExpress.ExpressApp.Security.SecurityModule.UsedExportedTypes = DevExpress.Persistent.Base.UsedExportedTypes.Custom;
            AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Analysis));

            // Register entites from another assembly
            AdditionalExportedTypes.AddRange(
                ModuleHelper.CollectExportedTypesFromAssembly(
                    typeof(Invoice).Assembly,
                    type => type.Namespace.StartsWith("Apteka.Model.Entities")));
        }

        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }

        public override void Setup(XafApplication application)
        {
            base.Setup(application);
            // Manage various aspects of the application UI and behavior at the module level.
        }

        public override void CustomizeTypesInfo(ITypesInfo typesInfo)
        {
            base.CustomizeTypesInfo(typesInfo);

            foreach (var typeInfo in typesInfo.PersistentTypes)
            {
                // Add AggregatedAttribute to composition properties
                foreach (var memberInfo in typeInfo.OwnMembers.Where(m => m.IsProperty))
                {
                    var prop = typeInfo.Type.GetProperty(memberInfo.Name);
                    var oppInfo = memberInfo.AssociatedMemberInfo;
                    if (oppInfo != null)
                    {
                        var opp = oppInfo.Owner.Type.GetProperty(oppInfo.Name);
                        if (opp.GetCustomAttribute<RequiredAttribute>() != null)
                        {
                            memberInfo.AddAttribute(new AggregatedAttribute(), true);
                        }
                    }
                }

                // Add uniqueness checks
                var propAttrs = from prop in typeInfo.OwnMembers
                                select new { prop, attr = prop.Owner.Type.GetProperty(prop.Name).GetCustomAttribute<UniqueIndexAttribute>() };
                var keys = from pa in propAttrs
                           where pa.attr != null
                           group pa by pa.attr.Name;
                foreach (var key in keys)
                {
                    if (key.Count() == 1)
                    {
                        key.First().prop.AddAttribute(new RuleUniqueValueAttribute(), true);
                    }
                    else
                    {
                        typeInfo.AddAttribute(new RuleCombinationOfPropertiesIsUniqueAttribute(
                            String.Join(", ", key.OrderBy(k => k.attr.Order).Select(k => k.prop.Name))));
                    }
                }

                typesInfo.RefreshInfo(typeInfo);
            }
        }

        public override void AddGeneratorUpdaters(ModelNodesGeneratorUpdaters updaters)
        {
            base.AddGeneratorUpdaters(updaters);
            updaters.Add(new ModelClassUpdater());
            updaters.Add(new ModelMemberUpdater());
            updaters.Add(new ModelViewsGeneratorUpdater());
            updaters.Add(new DetailViewLayoutGeneratorUpdater());
            updaters.Add(new NavigationItemGeneratorUpdater());
        }
    }
}
