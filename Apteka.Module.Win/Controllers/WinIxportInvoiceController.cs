using System;
using System.IO;

using Apteka.Model.Entities;
using Apteka.Module.Ixporters;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace Apteka.Module.Controllers
{
    public class WinIxportInvoiceController : ObjectViewController<ListView, Invoice>
    {
        private readonly SimpleAction importAction;
        private readonly SimpleAction exportAction;
        private const string fileFilter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";

        public WinIxportInvoiceController()
        {
            importAction = new SimpleAction(this, "ImportInvoice", PredefinedCategory.Edit)
            {
                SelectionDependencyType = SelectionDependencyType.Independent
            };
            importAction.Execute += ImportAction_Execute;

            exportAction = new SimpleAction(this, "ExportInvoice", PredefinedCategory.Edit)
            {
                SelectionDependencyType = SelectionDependencyType.RequireSingleObject
            };
            exportAction.Execute += ExportAction_Execute;
        }

        private void ImportAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog()
            {
                DefaultExt = ".xml",
                Filter = fileFilter,
                CheckFileExists = true,
                Multiselect = true
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var fileName in dialog.FileNames)
                {
                    using (var file = File.OpenRead(fileName))
                    {
                        Import(file);
                    }
                }
                //using (var file = dialog.OpenFile())
                //{
                //    Import(file);
                //}
            }
        }

        private void ExportAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var dialog = new System.Windows.Forms.SaveFileDialog()
            {
                DefaultExt = ".xml",
                Filter = fileFilter
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (var file = dialog.OpenFile())
                {
                    InvoiceXmlIxporter.Export(file, View.ObjectSpace, (Invoice)e.CurrentObject);
                }
            }
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            View.EditorChanged += View_EditorChanged;
            SetupEditor();
        }

        private void View_EditorChanged(object sender, EventArgs e)
        {
            SetupEditor();
        }

        private void SetupEditor()
        {
            if (View.Editor != null)
            {
                View.Editor.ControlsCreated += Editor_ControlsCreated;
                SetupDragDrop();
            }
        }

        private void Editor_ControlsCreated(object sender, EventArgs e)
        {
            SetupDragDrop();
        }

        private void SetupDragDrop()
        {
            if (View.Editor is GridListEditor editor && editor.GridView != null)
            {
                editor.GridView.GridControl.AllowDrop = true;
                //editor.GridView.GridControl.MouseDown += GridControl_MouseDown;
                editor.GridView.GridControl.DragEnter += GridControl_DragEnter;
                editor.GridView.GridControl.DragDrop += GridControl_DragDrop;
            }
        }

        //private void GridControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        //{
        //    if (sender is GridControl grid && View.SelectedObjects.Count > 0)
        //    {
        //        string temp = Path.GetTempPath();
        //        var fileNames = new List<string>();
        //        foreach (Invoice invoice in View.SelectedObjects)
        //        {
        //            string name = String.Format("Invoice_{0:yyyy-MM-dd}_{1}.xml", invoice.DocDateTime, invoice.Code);
        //            string path = Path.Combine(temp, name);
        //            using (var file = File.OpenWrite(path))
        //            {
        //                InvoiceXmlImporter.Export(file, View.ObjectSpace, invoice);
        //            }
        //            fileNames.Add(path);
        //        }
        //        grid.DoDragDrop(
        //            new System.Windows.Forms.DataObject(System.Windows.Forms.DataFormats.FileDrop, fileNames),
        //            System.Windows.Forms.DragDropEffects.Copy);
        //    }
        //}

        private void GridControl_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
            {
                e.Effect = System.Windows.Forms.DragDropEffects.Copy;
            }
            else
            {
                e.Effect = System.Windows.Forms.DragDropEffects.None;
            }
        }

        private void GridControl_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] fileNames = (string[])e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop);
            foreach (string fileName in fileNames)
            {
                using (var file = File.OpenRead(fileName))
                {
                    Import(file);
                }
            }
        }

        private void Import(Stream file)
        {
            try
            {
                InvoiceXmlIxporter.Import(file, View.ObjectSpace, View.CollectionSource);
            }
            catch (ValidationException)
            {
                // User already saw an error message, so don't show a second one
            }
            catch (Exception ex)
            {
                WinApplication.Messaging.ShowExtendedException("Не удалось импортировать накладную", ex);
            }
        }
    }
}
