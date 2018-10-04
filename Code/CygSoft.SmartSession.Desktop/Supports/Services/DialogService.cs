
using GalaSoft.MvvmLight.Views;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CygSoft.SmartSession.Desktop.Supports.Services
{
    public class DialogService : IDialogViewService
    {
        public bool SelectFile(string initialDirectory, out string filePath)
        {
            return SelectFile(initialDirectory, null, null, out filePath);
        }

        public bool SelectFile(string initialDirectory, string defaultExtension, string fileFilters, out string filePath)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.Title = "Open File";
            dlg.DefaultExt = defaultExtension; // ".png";
            dlg.Filter = fileFilters; // "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            dlg.InitialDirectory = initialDirectory;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filePath = dlg.FileName;
                return true;
            }

            filePath = null;
            return false;
        }

        //public void ShowError(Exception Error, string Title)
        //{
        //    MessageBox.Show(Error.ToString(), Title, MessageBoxButton.OK, MessageBoxImage.Error);
        //}

        //public void ShowError(string Message, string Title)
        //{
        //    MessageBox.Show(Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
        //}

        //public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ShowInfo(string Message, string Title)
        //{
        //    MessageBox.Show(Message, Title, MessageBoxButton.OK, MessageBoxImage.Information);
        //}

        //public void ShowMessage(string Message, string Title)
        //{
        //    MessageBox.Show(Message, Title, MessageBoxButton.OK);
        //}

        //public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task ShowMessageBox(string message, string title)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool ShowQuestion(string Message, string Title)
        //{
        //    return MessageBox.Show(Message, Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        //}

        //public void ShowWarning(string Message, string Title)
        //{
        //    MessageBox.Show(Message, Title, MessageBoxButton.OK, MessageBoxImage.Warning);
        //}

        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessage(string message, string title)
        {
            MessageBox.Show(message, message, MessageBoxButton.OK, MessageBoxImage.Information);
            return null;
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessageBox(string message, string title)
        {
            throw new NotImplementedException();
        }
    }
}
