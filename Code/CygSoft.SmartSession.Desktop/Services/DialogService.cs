
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;

namespace CygSoft.SmartSession.Desktop.Services
{
    public class DialogService : IDialogService
    {
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

        //Task IDialogService.ShowMessage(string message, string title)
        //{
        //    MessageBox.Show(message, message, MessageBoxButton.OK, MessageBoxImage.Information);
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
