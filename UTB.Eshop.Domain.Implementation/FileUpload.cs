using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using UTB.Eshop.Domain.Abstraction;

namespace UTB.Eshop.Domain.Implementation
{
    //3 interface v jedne tride jsou pro zjednoduseni, protoze jeste nemame aplikacni vrstvu, ale obecne se vytvari implementace (trida) pro kazdy interface zvlast, abychom splnili SOLID principy.

    public class FileUpload : IFileUpload, ICheckFileContent, ICheckFileLength  //<><>
    {

        public string RootPath { get; set; }

        public string ContentType { get; set; } //<><>
        public long FileLength { get; set; } //<><>


        public FileUpload(string rootPath)
        {
            this.RootPath = rootPath;
        }


        public bool CheckFileContent(IFormFile iFormFile, string contentTypeRequired)
        {
            return iFormFile != null && iFormFile.ContentType.ToLower().Contains(contentTypeRequired.ToLower());
        }


        public bool CheckFileLength(IFormFile iFormFile, long maxLengthFile = 2_000_000)
        {
            return iFormFile.Length > 0 && iFormFile.Length < maxLengthFile;
        }


        public async Task<string> FileUploadAsync(IFormFile iFormFile, string folderNameOnServer)
        {
            string filePathOutput = String.Empty;
            if (CheckFileContent(iFormFile, this.ContentType) && CheckFileLength(iFormFile, this.FileLength)) //<><>
            {
                var fileName = Path.GetFileNameWithoutExtension(iFormFile.FileName);
                var fileExtension = Path.GetExtension(iFormFile.FileName);
                //var fileNameGenerated = Path.GetRandomFileName();

                var fileRelative = Path.Combine(folderNameOnServer, fileName + fileExtension);
                var filePath = Path.Combine(this.RootPath, fileRelative);

                Directory.CreateDirectory(Path.Combine(this.RootPath, folderNameOnServer));
                using (Stream stream = new FileStream(filePath, FileMode.Create))   //<><>
                {
                    await iFormFile.CopyToAsync(stream);
                }

                filePathOutput = $"/{fileRelative}";
            }

            return filePathOutput;
        }
    }
}
