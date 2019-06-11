using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.XtraBars.Docking2010.Views;

namespace Apteka.Module.Win.Controllers
{
    // https://www.devexpress.com/Support/Center/Question/Details/Q438709/how-to-handle-a-moment-when-an-active-tab-is-changed-in-the-tabbed-mdi-mode
    // TODO: It doesn't work for multiple windows (detached from main window)
    public class CustomWinWindowTemplateController : WinWindowTemplateController
    {
        protected override void OnDocumentActivated(DocumentEventArgs e)
        {
            base.OnDocumentActivated(e);
            if (e.Document.Form is IViewHolder viewHolder &&
                viewHolder.View is ListView listView)
            {
                UpdateStatus(listView);
            }
            else
            {
                ClearStatus();
            }
        }

        protected override void AddViewListeners(View view)
        {
            base.AddViewListeners(view);
            if (view is ListView listView)
            {
                listView.ItemsChanged += ListView_ItemsChanged;
                listView.SelectionChanged += ListView_SelectionChanged;
                if (listView.Editor is GridListEditor gridListEditor)
                {
                    gridListEditor.GridView.RowCountChanged += (s, e) =>
                    {
                        UpdateStatus(listView);
                    };
                }
            }
        }

        protected override void RemoveViewListeners(View view)
        {
            if (view is ListView listView)
            {
                listView.ItemsChanged -= ListView_ItemsChanged;
                listView.SelectionChanged -= ListView_SelectionChanged;
            }
            base.RemoveViewListeners(view);
        }

        private void ListView_ItemsChanged(object sender, ViewItemsChangedEventArgs e)
        {
            UpdateStatus((ListView)sender);
        }

        private void ListView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateStatus((ListView)sender);
        }

        private void UpdateStatus(ListView listView)
        {
            if (Application.MainWindow == null) { return; }
            var statuses = new List<String>
            {
                String.Format("Всего записей: {0}", listView.CollectionSource.GetCount())
            };
            if (listView.Editor is GridListEditor gridListEditor)
            {
                statuses.Add(String.Format("Найдено: {0}", gridListEditor.GridView.RowCount));
            }
            statuses.Add(String.Format("Выбрано: {0}", listView.SelectedObjects.Count));
            Application.MainWindow.Template.SetStatus(statuses);
            //var mainWindow = Application.MainWindow;
            //var template = mainWindow.Template;
            //template.SetStatus(statuses);
        }

        private void ClearStatus()
        {
            if (Application.MainWindow == null) { return; }
            Application.MainWindow.Template.SetStatus(new string[] { });
            //var mainWindow = Application.MainWindow;
            //var template = mainWindow.Template;
            //template.SetStatus(new string[] { });
        }
    }
}
