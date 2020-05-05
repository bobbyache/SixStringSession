using CygSoft.SmartSession.Dal.MySql.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.Supports
{
    public class Settings : ISettings
    {
        // Replace this in order to swap out AppSettings during unit testing!
        public static ISettings AppSettings { get; set; } = new Settings();

        public virtual Version AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        private IDbConnection databaseConnection;
        public virtual IDbConnection DatabaseConnection
        {
            get
            {
                if (databaseConnection == null)
                {
                    var connectionManager = new ConnectionManager(ConnectionString);
                    databaseConnection = connectionManager.GetConnection();
                }

                return databaseConnection;
            }
        }

        public virtual string ConnectionString
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
                    return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                return "";
            }
        }

        private string fileAttachmentFolder;
        public virtual string FileAttachmentFolder
        {
            get
            {
                if (string.IsNullOrEmpty(fileAttachmentFolder))
                    fileAttachmentFolder = ConfigurationManager.AppSettings["FileAttachmentFolder"];

                return fileAttachmentFolder;
            }
            set
            {
                ConfigurationManager.AppSettings["FileAttachmentFolder"] = value;
            }
        }

        public virtual string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public virtual string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public virtual string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public virtual string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public virtual string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
    }
}
