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

            dialog = new SaveFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Title = "Choice where to save the new Database File";
            dialog.Filter = "sqlite files (*.sqlite)|*.sqlite";
            dialog.DefaultExt = "sqlite";

        }

        public void Show()
        {

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                onFileCreate(dialog.FileName);
            }

        }

    }
}
