﻿using System.Linq;

using Apteka.Model;
using Apteka.Model.Extensions;

using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;

namespace Apteka.Module.ModelExtensions
{
    // https://www.devexpress.com/Support/Center/Question/Details/T399534/rulecriteria-with-localized-templated-setting-localized-vales-with-the
    // https://www.devexpress.com/Support/Center/Question/Details/Q472121/localized-text-in-nodes-generator-updater
    // https://www.devexpress.com/Support/Center/Question/Details/S92247/how-to-localize-layout-group-captions-with-a-class-name
    public class ModelClassCaptionUpdater : ModelNodesGeneratorUpdater<ModelBOModelClassNodesGenerator>
    {
        public override void UpdateNode(ModelNode node)
        {
            if (node.Application is ModelApplicationBase application &&
                node is IModelBOModel classes)
            {
                string currentAspect = application.GetAspect(application.GetCurrentAspectIndex());
                try
                {
                    application.SetCurrentAspect("ru");
                    foreach (var cls in classes)
                    {
                        var attr = cls.TypeInfo.Type
                            .GetCustomAttributes(typeof(DataElementAttribute), false)
                            .OfType<DataElementAttribute>().FirstOrDefault();
                        if (attr != null)
                        {
                            // Update class caption
                            cls.Caption = attr.Name.FirstCharToUpper();

                            // Update list view caption
                            cls.DefaultListView.Caption = attr.PluralName.FirstCharToUpper();

                            // Update list view column captions
                            foreach (var col in cls.DefaultListView.Columns)
                            {
                                var mi = col.ModelMember.MemberInfo;
                                var prop = mi.Owner.Type.GetProperty(mi.Name);
                                var memberAttr = prop.GetCustomAttributes(typeof(DataElementAttribute), false)
                                    .OfType<DataElementAttribute>().FirstOrDefault();
                                if (memberAttr != null)
                                {
                                    col.Caption = memberAttr.BriefName.FirstCharToUpper();
                                }
                            }
                        }
                    }
                }
                finally
                {
                    application.SetCurrentAspect(currentAspect);
                }
            }
        }
    }
}
