using System.Linq;

using Apteka.Model;
using Apteka.Model.Extensions;

using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;

namespace Apteka.Module.ModelExtensions
{
    public class ModelMemberCaptionUpdater : ModelNodesGeneratorUpdater<ModelBOModelMemberNodesGenerator>
    {
        public override void UpdateNode(ModelNode node)
        {
            if (node.Application is ModelApplicationBase application &&
                node is IModelBOModelClassMembers members)
            {
                string currentAspect = application.GetAspect(application.GetCurrentAspectIndex());
                try
                {
                    application.SetCurrentAspect("ru");
                    foreach (var m in members)
                    {
                        var prop = m.MemberInfo.Owner.Type.GetProperty(m.MemberInfo.Name);
                        var attr = prop.GetCustomAttributes(typeof(DataElementAttribute), false)
                            .OfType<DataElementAttribute>().FirstOrDefault();
                        if (attr != null)
                        {
                            m.Caption = attr.Name.FirstCharToUpper();
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
