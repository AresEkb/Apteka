using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.SystemModule;

namespace Apteka.Module.ModelExtensions
{
    public class NavigationItemGeneratorUpdater : ModelNodesGeneratorUpdater<NavigationItemNodeGenerator>
    {
        public override void UpdateNode(ModelNode node)
        {
            if (node is IModelRootNavigationItems root)
            {
                foreach (var cls in node.Application.BOModel)
                {
                    var attr = cls.TypeInfo.Type
                        .GetCustomAttributes<CategoryAttribute>(false)
                        .FirstOrDefault();
                    if (attr != null && !String.IsNullOrWhiteSpace(attr.Category))
                    {
                        var items = root.Items;
                        foreach (var step in attr.Category.Split('/'))
                        {
                            var category = items[step];
                            if (category == null)
                            {
                                category = items.AddNode<IModelNavigationItem>(step);
                            }
                            items = category.Items;
                        }
                        var item = items.AddNode<IModelNavigationItem>(cls.DefaultListView.Id);
                        item.View = cls.DefaultListView;
                    }
                }
            }
        }
    }
}
