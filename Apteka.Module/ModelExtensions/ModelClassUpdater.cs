using System.Linq;
using System.Reflection;

using Apteka.Model.Annotations;
using Apteka.Model.Extensions;
using Apteka.Module.Extensions;

using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;

namespace Apteka.Module.ModelExtensions
{
    // https://www.devexpress.com/Support/Center/Question/Details/T399534/rulecriteria-with-localized-templated-setting-localized-vales-with-the
    // https://www.devexpress.com/Support/Center/Question/Details/Q472121/localized-text-in-nodes-generator-updater
    // https://www.devexpress.com/Support/Center/Question/Details/S92247/how-to-localize-layout-group-captions-with-a-class-name
    public class ModelClassUpdater : ModelNodesGeneratorUpdater<ModelBOModelClassNodesGenerator>
    {
        public override void UpdateNode(ModelNode node)
        {
            var classes = (IModelBOModel)node;
            var application = (ModelApplicationBase)node.Application;
            string currentAspect = application.GetAspect(application.GetCurrentAspectIndex());
            try
            {
                application.SetCurrentAspect("ru");
                foreach (var cls in classes)
                {
                    var attr = cls.TypeInfo.Type
                        .GetCustomAttributes<DataElementAttribute>(false)
                        .FirstOrDefault();
                    if (attr != null)
                    {
                        // Update class caption
                        cls.Caption = attr.Name.FirstCharToUpper();
                        // Update list view caption
                        cls.DefaultListView.Caption = attr.PluralName.FirstCharToUpper();
                        // Update list view column captions
                        cls.DefaultListView.UpdateColumnCaptions();
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
