using System;
using System.Windows;

namespace SmartGoals.Services
{
    public class DialogService : IDialogService
    {
        public bool OpenFile(string initialDirectory, out string filePath)
        {
            return OpenFile(initialDirectory, null, null, out filePath);
        }

        public bool OpenFile(string initialDirectory, string defaultExtension, string fileFilters, out string filePath)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.Title = "Open File";
            dlg.DefaultExt = defaultExtension; // ".png";
            dlg.Filter = fileFilters; // "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            dlg.InitialDirectory = initialDirectory;
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;
            dlg.Multiselect = false;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filePath = dlg.FileName;
                return true;
            }

            filePath = null;
            return false;
        }

        public bool YesNoPrompt(string caption, string message)
        {
            MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                return true;
            return false;
        }

        public bool WarningYesNoPrompt(string caption, string message)
        {
            MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
                return true;
            return false;
        }

        public void ExclamationMessage(string caption, string message)
        {
            MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
