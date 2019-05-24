using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apteka.Module.BusinessObjects;
using Apteka.Module.Importers;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;

namespace Apteka.Module.Controllers
{
    public class WinExportInvoiceController : ObjectViewController<ListView, Invoice>
    {
        private readonly SimpleAction action;

        public WinExportInvoiceController()
        {
            action = new SimpleAction(this, "ExportInvoice", PredefinedCategory.Edit)
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
