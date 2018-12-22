using System;
using System.Windows.Forms;

namespace MyMoney.Windows.Components.DBFileDialogs
{
    public class DBCreateFileDialog
    {
        private readonly SaveFileDialog dialog;

        private readonly Action<string> onFileCreate;

        public DBCreateFileDialog(Action<string> onFileCreate)
        {
            this.onFileCreate = onFileCreate;

            dialog = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = "Choice where to save the new Database File",
                Filter = "database files (*.mdf)|*.mdf",
                DefaultExt = "mdf"
            };

        }

        public void Show()
        {

            if (dialog.ShowDialog() != DialogResult.OK) return;

            onFileCreate(dialog.FileName);

        }

    }
}
