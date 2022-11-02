using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTB.Eshop.Domain.Abstraction
{
    public interface ICheckFileContent
    {
        bool CheckFileContent(IFormFile iFormFile, string contentTypeRequired);
    }
}
