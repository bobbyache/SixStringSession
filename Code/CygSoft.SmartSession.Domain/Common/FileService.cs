using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Common
{
    public class FileService : IFileService
    {
        public string FolderPath { get; }

        public FileService(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("Folder path must be specified.");
            FolderPath = folderPath;
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(Path.Combine(FolderPath, fileName));
        }

        public void Copy(string sourcefile, string destinationfile)
        {
            File.Copy(sourcefile, destinationfile);
        }
    }
}
