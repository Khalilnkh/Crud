using System;
namespace P335_BackEnd.Services
{
    public class FileService
    {
        public List<string> AddFile(List<IFormFile> files, string targetDirectory)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("wwwroot", targetDirectory));

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var uploadedFileNames = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileInfo = new FileInfo(file.FileName);
                    string fileExtension = fileInfo.Extension.ToLower();



                    string fileName = fileInfo.Name + Guid.NewGuid().ToString() + fileExtension;
                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    uploadedFileNames.Add(fileName);
                }
            }

            return uploadedFileNames;
        }


        public void DeleteFile(string fileName, string targetDirectory)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("wwwroot", targetDirectory, fileName));

            if (!Path.Exists(path)) return;

            File.Delete(path);
        }
    }
}

