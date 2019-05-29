using System.Drawing;

using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;

namespace Apteka.Module.ModelExtensions
{
    // https://docs.devexpress.com/eXpressAppFramework/112641/task-based-help/views/how-to-implement-a-view-item
    public class DetailViewLayoutGeneratorUpdater : ModelNodesGeneratorUpdater<ModelDetailViewLayoutNodesGenerator>
    {
        public override void UpdateNode(ModelNode node)
        {
            var layoutNode = (IModelViewLayout)node;
            if (layoutNode.GetNode(ModelDetailViewLayoutNodesGenerator.MainLayoutGroupName) is IModelLayoutGroup mainGroup)
            {
                if (mainGroup.GetNode(ModelDetailViewLayoutNodesGenerator.SizeableEditorsLayoutGroupName) is IModelLayoutGroup sizeableGroup)
                {
                    foreach (IModelLayoutViewItem item in sizeableGroup)
                    {
                        item.SizeConstraintsType = XafSizeConstraintsType.Custom;
                        item.MaxSize = new Size(0, 64);
                    }
                }
            }
        }
    }
}
