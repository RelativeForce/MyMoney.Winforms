using System;
using System.Windows.Forms;

namespace MyMoney.Windows.Components.DBFileDialogs
{
    public class DBLoadFileDialog
    {

        private readonly Action<string> onFileSelect;

        private readonly OpenFileDialog dialog;

        public DBLoadFileDialog(Action<string> onFileSelect)
        {
            this.onFileSelect = onFileSelect;

            dialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Multiselect = false,
                Title = "Import Database File",
                DefaultExt = "sqlite",
                Filter = "sqlite files (*.sqlite)|*.sqlite",
                CheckFileExists = true,
                CheckPathExists = true
            };
        }

        public void Show()
        {

            if (dialog.ShowDialog() != DialogResult.OK) return;

            onFileSelect(dialog.FileName);

        }
    }
}
