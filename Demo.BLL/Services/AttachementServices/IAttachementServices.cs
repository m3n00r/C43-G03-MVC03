using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Services.AttachementServices
{
    public interface IAttachementServices
    {
        //Upload
        public string? Upload(IFormFile file , string FolderName);

        //Delete
        bool Delete(string filePath);


    }
}
