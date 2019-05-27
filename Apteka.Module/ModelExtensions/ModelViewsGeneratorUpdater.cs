using System.Linq;
using System.Reflection;

using Apteka.Model.Annotations;

using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;

namespace Apteka.Module.ModelExtensions
{
    // HACK: AggregatedAttribute is not applied to a property
    // So we have to hide link and unlink buttons for compositions explicitly
    // https://www.devexpress.com/Support/Center/Question/Details/T443746/enable-endless-pagin-on-all-nested-list-view-in-web-application
    public class ModelViewsGeneratorUpdater : ModelNodesGeneratorUpdater<ModelViewsNodesGenerator>
    {
        public override void UpdateNode(ModelNode node)
        {
            if (node is IModelViews views)
            {
                foreach (IModelView viewModel in views)
                {
                    if (viewModel is IModelDetailView detailView)
                    {
                        foreach (IModelViewItem viewItem in detailView.Items)
                        {
                            if (viewItem is IModelPropertyEditor propertyEditor &&
                                propertyEditor.PropertyEditorType == typeof(ListPropertyEditor))
                            {
                                CustomizeListPropertyEditor(propertyEditor);
                            }
                        }
                    }
                }
            }
        }

        private void CustomizeListPropertyEditor(IModelPropertyEditor propertyEditor)
        {
            if (propertyEditor.View is IModelListView listViewModel)
            {
                var mi = propertyEditor.ModelMember.MemberInfo;
                var prop = mi.Owner.Type.GetProperty(mi.Name);
                var attr = prop.GetCustomAttributes<CompositionAttribute>(false).FirstOrDefault();
                if (attr != null)
                {
                    listViewModel.AllowLink = false;
                    listViewModel.AllowUnlink = false;
                }
            }
        }
    }
}
