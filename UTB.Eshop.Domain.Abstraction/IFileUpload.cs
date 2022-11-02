using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTB.Eshop.Domain.Abstraction
{
    public interface IFileUpload
    {
        Task<string> FileUploadAsync(IFormFile iFormFile, string folderNameOnServer);

        string ContentType { get; set; } //<><>
        long FileLength { get; set; } //<><>
    }
}
