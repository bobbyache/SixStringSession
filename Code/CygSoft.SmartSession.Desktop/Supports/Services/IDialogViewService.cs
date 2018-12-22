using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Supports.Services
{
    public interface IDialogViewService : IDialogService
    {
        bool YesNoPrompt(string caption, string message);
        bool SelectFile(string initialDirectory, out string filePath);
        bool SelectFile(string initialDirectory, string defaultExtension, string fileFilters, out string filePath);

    }
}
