using Apteka.Module.BusinessObjects;
using Apteka.Module.Importers;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;

namespace Apteka.Module.Controllers
{
    public class WinImportInvoiceController : ObjectViewController<ListView, Invoice>
    {
        private readonly SimpleAction action;

        public WinImportInvoiceController()
        {
            action = new SimpleAction(this, "ImportInvoice", PredefinedCategory.Edit)
            {
                SelectionDependencyType = SelectionDependencyType.Independent
            };
            action.Execute += Action_Execute;
        }

        private void Action_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog()
            {
                DefaultExt = ".xml",
                Filter = "XML files (.xml)|*.xml"
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (var file = dialog.OpenFile())
                {
                    InvoiceXmlImporter.Import(file, View.ObjectSpace, View.CollectionSource);
                }
            }
        }
    }
}
