using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTB.Eshop.Domain.Abstraction
{
    public interface ICheckFileLength
    {
        bool CheckFileLength(IFormFile iFormFile, long maxLengthFile = 2_000_000);
    }
}
