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

            dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); ;
            dialog.Multiselect = false;
            dialog.Title = "Import Database File";
            dialog.DefaultExt = "sqlite";
            dialog.Filter = "sqlite files (*.sqlite)|*.sqlite";
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;

        }

        public void Show()
        {

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                onFileSelect(dialog.FileName);
            }

        }
    }
}
