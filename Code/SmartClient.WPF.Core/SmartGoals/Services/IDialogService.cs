
namespace SmartGoals.Services
{
    public interface IDialogService
    {
        bool YesNoPrompt(string caption, string message);
        void ExclamationMessage(string caption, string message);
        bool YesNoWarningPrompt(string caption, string message);
        bool OpenFile(string initialDirectory, out string filePath);
        bool OpenFile(string initialDirectory, string defaultExtension, string fileFilters, out string filePath);
        bool CreateFile(string initialDirectory, string defaultExtension, string fileFilters, out string filePath);
    }
}
