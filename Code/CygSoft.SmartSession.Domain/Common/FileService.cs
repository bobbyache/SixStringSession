using System;
using System.IO;
using System.Linq;

namespace CygSoft.SmartSession.Domain.Common
{
    public class FileService : IFileService
    {
        public string FolderPath { get; }

        public string ExerciseFolderPath => Path.Combine(FolderPath, "Exercises");

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

        public void Delete(string fileName)
        {
            File.Delete(Path.Combine(FolderPath, fileName));
        }

        public string GenerateFileId()
        {
            return Guid.NewGuid().ToString();
        }

        public void AddExerciseFiles(int id, string[] filePaths)
        {
            if (!Directory.Exists(FolderPath))
                throw new DirectoryNotFoundException("The application's root file directory could not be found.");

            if (!Directory.Exists(ExerciseFolderPath))
                Directory.CreateDirectory(ExerciseFolderPath);

            string fileFolder = Path.Combine(ExerciseFolderPath, id.ToString());

            if (!Directory.Exists(fileFolder))
                Directory.CreateDirectory(fileFolder);

            foreach (var file in filePaths)
            {
                var destinationPath = Path.Combine(fileFolder, Path.GetFileName(file));
                if (File.Exists(destinationPath))
                    throw new InvalidOperationException("A file with this name already exists and will be overwritten.");
            }

            foreach (var file in filePaths)
            {
                var destinationPath = Path.Combine(fileFolder, Path.GetFileName(file));
                File.Copy(file, destinationPath);
            }
        }

        public void DeleteExerciseFiles(int id, string[] fileNames)
        {
            string fileFolder = Path.Combine(ExerciseFolderPath, id.ToString());
            if (Directory.Exists(fileFolder))
            {
                foreach (var fileName in fileNames)
                {
                    var targetFile = Path.Combine(fileFolder, fileName);
                    File.Delete(targetFile);
                }
                if (EmptyExerciseFolder(fileFolder))
                    Directory.Delete(fileFolder, true);
            }
            else
                throw new DirectoryNotFoundException($"Could not find the exercise directory:\n {fileFolder}.");
        }

        public void DeleteExerciseFiles(int id)
        {
            string fileFolder = Path.Combine(ExerciseFolderPath, id.ToString());
            if (Directory.Exists(fileFolder))
            {
                foreach (var fileName in Directory.EnumerateFiles(fileFolder).ToArray())
                {
                    var targetFile = Path.Combine(fileFolder, fileName);
                    File.Delete(targetFile);
                }
                if (EmptyExerciseFolder(fileFolder))
                    Directory.Delete(fileFolder, true);
            }
            else
                throw new DirectoryNotFoundException($"Could not find the exercise directory:\n {fileFolder}.");
        }

        public string[] GetExerciseFiles(int id)
        {
            string fileFolder = Path.Combine(ExerciseFolderPath, id.ToString());
            if (Directory.Exists(fileFolder))
            {
                return Directory.EnumerateFiles(fileFolder).ToArray();
            }
            else
                throw new DirectoryNotFoundException($"Could not find the exercise directory:\n {fileFolder}.");
        }

        public void OpenExerciseFile(int id, string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start(fileName);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while trying to open the exercise file.", ex);
            }
        }

        private bool EmptyExerciseFolder(string exerciseFolderPath)
        {
            return Directory.GetFiles(exerciseFolderPath).Length == 0;
        }
    }
}
