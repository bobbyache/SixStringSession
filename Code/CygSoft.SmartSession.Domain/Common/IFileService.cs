using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Common
{
    public interface IFileService
    {
        string FolderPath { get; }
        bool FileExists(string fileName);
        void Copy(string sourcefile, string destinationfile);
        void Delete(string fileName);

        string GenerateFileId();

        void AddExerciseFiles(int id, string[] filePaths);
        void DeleteExerciseFiles(int id, string[] fileNames);

        void DeleteExerciseFiles(int id); // deletes all...

        string[] GetExerciseFiles(int id);

        void OpenExerciseFile(int id, string fileName);

    }
}
