using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Apteka.Model;
using Apteka.Module.ModelExtensions;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Updating;

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

            //AdditionalExportedTypes.Add(typeof(Model.Entities.InvoiceItem));

            AdditionalExportedTypes.AddRange(
                ModuleHelper.CollectExportedTypesFromAssembly(
                    typeof(Model.Entities.Invoice).Assembly,
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

        //public override void CustomizeTypesInfo(ITypesInfo typesInfo)
        //{
        //    base.CustomizeTypesInfo(typesInfo);
        //    foreach (var typeInfo in typesInfo.PersistentTypes)
        //    {
        //        // TODO: Remove in future
        //        if (!typeInfo.AssemblyInfo.FullName.StartsWith("Apteka."))
        //        {
        //            continue;
        //        }
        //        //var typeAttr = typeInfo.Type.CustomAttributes.OfType<DataElementAttribute>().FirstOrDefault();
        //        //var typeAttr = typeInfo.Type.GetCustomAttributes(typeof(DataElementAttribute), false)
        //        //    .OfType<DataElementAttribute>().FirstOrDefault();
        //        //if (typeAttr != null)
        //        //{
        //        //    typeInfo.AddAttribute(new DisplayNameAttribute(typeAttr.Name.FirstCharToUpper()));
        //        //}
        //        Console.WriteLine(">>>");
        //        foreach (var memberInfo in typeInfo.OwnMembers.Where(m => m.IsProperty))
        //        {
        //            var prop = typeInfo.Type.GetProperty(memberInfo.Name);
        //            var compAttr = prop.GetCustomAttributes<CompositionAttribute>(false).FirstOrDefault();
        //            if (compAttr != null)
        //            {
        //                //memberInfo.AddAttribute(new AssociationAttribute());
        //                memberInfo.AddAttribute(new AggregatedAttribute());
        //            }
        //            Console.WriteLine(memberInfo.Name);
        //            foreach (var a in memberInfo.Attributes)
        //            {
        //                Console.WriteLine(a);
        //            }
        //            //Console.WriteLine("!!!!!" + memberInfo.FindAttributes());
        //            //Console.WriteLine("!!!!!" + memberInfo.FindAttribute<AggregatedAttribute>());
        //            //Console.WriteLine("!!!!!" + prop.GetCustomAttributes<AggregatedAttribute>(false).FirstOrDefault());
        //        }
        //    }
        //}

        public override void AddGeneratorUpdaters(ModelNodesGeneratorUpdaters updaters)
        {
            base.AddGeneratorUpdaters(updaters);
            updaters.Add(new ModelClassUpdater());
            updaters.Add(new ModelMemberUpdater());
            updaters.Add(new ModelViewsGeneratorUpdater());
        }
    }
}
