using System;
using System.Data;

namespace CygSoft.SmartSession.Desktop.Supports
{
    public interface ISettings
    {
        string AssemblyCompany { get; }
        string AssemblyCopyright { get; }
        string AssemblyDescription { get; }
        string AssemblyProduct { get; }
        string AssemblyTitle { get; }
        Version AssemblyVersion { get; }
        string ConnectionString { get; }
        string FileAttachmentFolder { get; set; }

        IDbConnection DatabaseConnection { get; }
    }
}