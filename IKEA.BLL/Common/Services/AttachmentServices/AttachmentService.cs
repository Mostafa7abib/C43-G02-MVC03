using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace IKEA.BLL.Common.Services.AttachmentServices
{
    public class AttachmentService : IAttachmentService
    {
        //Allowed Extensions
        public readonly List<string> _allowedExtensions = new() {".png",".jpg",".jpeg" };
        //Max Size //2MB
        public const int _maxAllowedSize = 2_097_152;
        public string? Upload(IFormFile file, string folderName)
        {
            //1- Validadte on the extension
            var extension = Path.GetExtension(file.FileName);
            if(!_allowedExtensions.Contains(extension) )
                return null;
            //2- Validate on the Size
            if(file.Length > _maxAllowedSize)
                return null;
            //3- Get Located Folder Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwroot\\files\\",folderName);
            //4- Set Unique File Name
            var fileName = $"{Guid.NewGuid()}{extension}";
            //5- Combine Folder Path with File Name [FilePath]
            var filePath = Path.Combine(folderPath, fileName);
            //6- Save The File As Stream[Data Per Time]
            using var fileStream = new FileStream(filePath, FileMode.Create);
            //7- Copy The File To The Stream
            file.CopyTo(fileStream);
            //8- Return FileName
            return fileName;
        }
        public bool Delete(string filePath)
        {
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
