using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IKEA.BLL.Common.Services.AttachmentServices
{
    public interface IAttachmentService 
    {
        public string? Upload(IFormFile file, string folderName);
        public bool Delete(string filePath);
    }
}
